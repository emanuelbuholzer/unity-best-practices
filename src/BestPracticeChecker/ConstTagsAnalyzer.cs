using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;

    public class ConstTagsAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "ConstTags",
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.ConstTagsDescription)));
        
        public override void Initialize(AnalysisContext context)
        {
            var a = new List<string>() { "a" };
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;
            
            var method = Method.From(methodSymbol);
            
            var methodsArgumentExtractors = new HashSet<Tuple<Method, ArgumentExtractor>>()
            {
                Tuple.Create(
                    Method.From("UnityEngine", "GameObject", "FindWithTag"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Method.From("UnityEngine", "Application", "SetBuildTags"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression)) 
                    ),
                // TODO: Test
                Tuple.Create(
                    Method.From("UnityEngine.iOS", "OnDemandResourcesRequest", "PreloadAsync"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression)) 
                    )
            };

            var methodArgumentExtractor =
                methodsArgumentExtractors
                    .Where(m => m.Item1.Equals(method))
                    .Select(m => m.Item2)
                    .SingleOrDefault();
            if (methodArgumentExtractor == null)
                return;
            
            var argumentExpressions = methodArgumentExtractor.Invoke(invocationExpression.ArgumentList.Arguments);
            var isConstTagArguments = argumentExpressions
                .Select(a => context.SemanticModel.GetConstantValue(a, context.CancellationToken))
                .Select(r => r.HasValue);

            var missingConstTagArgumentExpressions = argumentExpressions
                .Zip(isConstTagArguments, Tuple.Create)
                .Where(t => !t.Item2)
                .Select(t => t.Item1);
            
            foreach (var argumentExpression in missingConstTagArgumentExpressions)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, argumentExpression.GetLocation()));
            }
        }
    }
}
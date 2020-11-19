﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace BestPracticeChecker
{
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class InefficientStringApiAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0007",
                DiagnosticStrings.GetString(nameof(Strings.InefficientStringApiTitle)),
                DiagnosticStrings.GetString(nameof(Strings.InefficientStringApiMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0007_InefficientStringMethods.md"));

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var method = Symbol.From(methodSymbol);

            var methodsArgumentExtractors = new HashSet<Tuple<Symbol, ArgumentExtractor>>()
            {
                Tuple.Create(
                    Symbol.From("System", "String", "EndsWith"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("System", "String", "StartsWith"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    )
            };

            // Unique method signature using two arguments => Ordinal Comparison
            if (invocationExpression.ArgumentList.Arguments.Count == 2)
                return;

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

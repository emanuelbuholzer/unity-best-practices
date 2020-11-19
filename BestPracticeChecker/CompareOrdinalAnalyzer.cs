﻿using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BestPracticeChecker
{
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CompareOrdinalAnalyzer : DiagnosticAnalyzer
    {

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0006",
                DiagnosticStrings.GetString(nameof(Strings.CompareStringWithOrdinalTitle)),
                DiagnosticStrings.GetString(nameof(Strings.CompareStringWithOrdinalMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: DiagnosticStrings.GetString(nameof(Strings.CompareStringWithOrdinalDescription)));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
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
                    Symbol.From("System", "String", "Equals"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("System", "String", "Compare"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    )
            };

            // method signature using three arguments ==> Overload Equal wir StringComparison
            if (invocationExpression.ArgumentList.Arguments.Count == 3)
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

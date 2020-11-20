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
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0006_OrdinalStringComparison.md"));
        
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

            var methods= new List<Symbol>()
            {
                Symbol.From("System", "String", "Equals"),
                Symbol.From("System", "String", "Compare"),
            };
            
            if (!methods.Any(m => m.Equals(methodSymbol)))
                return;

            if (Symbol.From("System", "String", "Equals").Equals(methodSymbol) &&
                invocationExpression.ArgumentList.Arguments.Count == 2)
                return;

            // method signature using three arguments ==> Overload Equal wir StringComparison
            if (invocationExpression.ArgumentList.Arguments.Count == 3)
            {
                var allowedComparisons = new List<string>()
                {
                    "StringComparison.Ordinal",
                    "StringComparison.OrdinalIgnoreCase"
                };
                
                if (allowedComparisons.Any(a => 
                    a.Equals(invocationExpression.ArgumentList.Arguments[2].Expression.GetText().ToString())))
                    return;
            }
            
            context.ReportDiagnostic(Diagnostic.Create(Rule, invocationExpression.GetLocation()));
        }
    }
}

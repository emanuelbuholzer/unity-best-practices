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
    public class StringReferencesAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0003",
                DiagnosticStrings.GetString(nameof(Strings.StringReferencesTitle)),
                DiagnosticStrings.GetString(nameof(Strings.StringReferencesMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0003_StringReferences.md"));

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var methods = new List<Symbol>()
            {
                Symbol.From("UnityEngine", "MonoBehaviour", "StartCoroutine"),
                Symbol.From("UnityEngine", "MonoBehaviour", "StopCoroutine"),
            };

            if (!methods.Any(m => m.Equals(methodSymbol)))
                return;

            var firstParameterType = methodSymbol.Parameters.FirstOrDefault().Type;
            if ("String".Equals(firstParameterType.Name) && "System".Equals(firstParameterType.ContainingNamespace.ToString()))
                context.ReportDiagnostic(Diagnostic.Create(Rule, invocationExpression.GetLocation()));
        }

    }
}

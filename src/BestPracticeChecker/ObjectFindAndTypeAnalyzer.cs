using System.Collections.Immutable;
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
    public class ObjectFindAndTypeAnalyzer : DiagnosticAnalyzer
    {

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0022",
                DiagnosticStrings.GetString(nameof(Strings.ObjectFindAndTypeTitle)),
                DiagnosticStrings.GetString(nameof(Strings.ObjectFindAndTypeMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: DiagnosticStrings.GetString(nameof(Strings.ObjectFindAndTypeDescription)),
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0002_ConditionalDebug.md"));


        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var method = ImmutableList.Create(
                Symbol.From("UnityEngine", "GameObject", "Find"),
                Symbol.From("UnityEngine", "Object", "FindObjectOfType"));

            if (!method.Any(m => m.Equals(Symbol.From(methodSymbol))))
                return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}

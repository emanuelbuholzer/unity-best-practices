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
    public class ObjectFindAndTypeAnalyzer: SyntaxNodeInMethodAnalyzer
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


        protected override SyntaxKind ByKind => SyntaxKind.SimpleMemberAccessExpression;
        protected override ImmutableList<Symbol> InMethods =>
            ImmutableList.Create(
                Symbol.From("UnityEngine", "MonoBehaviour", "Start"),
                Symbol.From("UnityEngine", "MonoBehaviour", "Awake"),
                Symbol.From("UnityEngine", "GameObject", "Find"),
                Symbol.From("UnityEngine", "Object", "FindObjectOfType")
                );

        protected override ImmutableList<Symbol> FilterBySymbols =>
            ImmutableList.Create(
                Symbol.From("UnityEngine", "GameObject", "Find"),
                Symbol.From("UnityEngine", "Object", "FindObjectOfType"));

        public override void AnalyzeNodeInMethod(SemanticModelAnalysisContext context, SyntaxNode node)
        {
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation()));
        }
    }
}

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System.Linq;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SmallClassesAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0008",
                DiagnosticStrings.GetString(nameof(Strings.SmallClassesTitle)),
                DiagnosticStrings.GetString(nameof(Strings.SmallClassesMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.CodeStyle,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0008_SmallClasses.md"));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze |
                                                   GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var classLines = context.Node.ChildNodes().Select(n => n.GetText().Lines.Count).Sum();
            if (classLines < 100)
                return;
            
            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}

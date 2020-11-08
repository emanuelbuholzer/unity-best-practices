using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CamaraMainInUpdateAnalyzer : SyntaxNodeInMethodAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0023",
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.ConstTagsDescription)),
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0023_MainCameraInUpdate.md"));
        
        protected override SyntaxKind ByKind => SyntaxKind.SimpleMemberAccessExpression; 
        protected override ImmutableList<Symbol> InMethods => 
            ImmutableList.Create(Symbol.From("UnityEngine", "MonoBehaviour", "Update"));

        protected override ImmutableList<Symbol> FilterBySymbols =>
            ImmutableList.Create(Symbol.From("UnityEngine", "Camera", "main"));

        public override void AnalyzeNodeInMethod(SemanticModelAnalysisContext context, SyntaxNode node)
        {
            context.ReportDiagnostic(Diagnostic.Create(Rule, node.GetLocation())); 
        }
    }
}
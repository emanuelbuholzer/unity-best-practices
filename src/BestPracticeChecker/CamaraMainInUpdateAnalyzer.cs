using System.Collections.Immutable;
using System.Linq;
using System.Xml.Schema;
using BestPracticeChecker.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CamaraMainInUpdateAnalyzer : DiagnosticAnalyzer
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
                description: DiagnosticStrings.GetString(nameof(Strings.ConstTagsDescription)));
        
        private MethodExtractor _methodExtractor;
        
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            _methodExtractor = MethodExtractor.Register(context);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.SimpleMemberAccessExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var memberAccessExpression = (MemberAccessExpressionSyntax) context.Node;
            var symbol = context.SemanticModel.GetSymbolInfo(memberAccessExpression).Symbol as IPropertySymbol;

            var cameraMainSymbol = Symbol.From("UnityEngine", "Camera", "main");
            if (!cameraMainSymbol.Equals(symbol))
                return;

            var inMethodSearch = NodeInMethodSearch.Create().WithNode(memberAccessExpression);
            var updateMethodLocations = _methodExtractor.GetLocationsByBaseTypeMethod(
                Symbol.From("UnityEngine", "MonoBehaviour", "Update")
            );
            if (!updateMethodLocations.Any(l => inMethodSearch.WithMethodLocation(l).Search().InMethod))
                return;
           
            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}
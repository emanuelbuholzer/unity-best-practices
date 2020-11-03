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
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class GetComponentAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(
                "GetComponent", 
                DiagnosticStrings.GetString(nameof(Strings.GetComponentTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.GetComponentMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.GetComponentDescription)));

        private readonly List<Location> _updateMethodLocations = new List<Location>();

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
            context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.Method);
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var symbol = context.Symbol;

            var baseType = symbol.ContainingType.BaseType;
            if (baseType == null)
                return;
            if (baseType.ContainingNamespace.Name != "UnityEngine" && baseType.ContainingType.Name != "MonoBehaviour")
                return;

            if (symbol.MetadataName == "Update")
                _updateMethodLocations.AddRange(symbol.Locations);
        }
        
        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;
            
            var methodSymbolMatcher = MethodSymbolMatcher.FromMethodSymbol(methodSymbol);
            var methods = new List<string> { "GetComponent", "GetComponents" }; 
            if (!methods.Any(m => methodSymbolMatcher.MatchMethod("UnityEngine", "GameObject", m)))
                return;
            
            var ancestorSourceSpans = invocationExpression.Ancestors()
                .Where(a => a.Kind() == SyntaxKind.MethodDeclaration)
                .Select(a => a.GetLocation().SourceSpan);

            if (!ancestorSourceSpans.Any(l => _updateMethodLocations.Any(a => l.Contains(a.SourceSpan))))
                return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}
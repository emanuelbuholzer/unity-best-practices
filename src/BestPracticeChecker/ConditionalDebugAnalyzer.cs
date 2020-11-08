using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System.Linq;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConditionalDebugAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(
                "BP0002", 
                DiagnosticStrings.GetString(nameof(Strings.ConditionalDebugTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.ConditionalDebugMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Correctness, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.ConditionalDebugDescription)),
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0002_ConditionalDebug.md")
            );

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var symbol = Symbol.From(methodSymbol);
            
            var namespaceName = methodSymbol.ContainingNamespace.Name;
            if (namespaceName != "UnityEngine")
                return;

            var typeName = methodSymbol.ContainingType.Name;
            if (typeName != "Debug")
                return;

            var methodName = methodSymbol.Name;

            var methods = ImmutableList.Create(
                "Log",
                "LogAssertion",
                "LogError",
                "LogException",
                "LogWarning",
                "LogAssertionFormat",
                "LogErrorFormat",
                "LogFormat",
                "LogWarningFormat"
            ).Select(n => Symbol.From("UnityEngine", "Debug", n));

            var isLogMethod = methods.Any(m => m.Equals(symbol));
            if (isLogMethod != true)
                return;

            var outerMethodDeclaration = invocationExpression.FirstAncestorOrSelf<MethodDeclarationSyntax>();
            if (outerMethodDeclaration == null)
                return;

            var hasConditionalAttribute = outerMethodDeclaration
                .AttributeLists.SelectMany(attributeList => attributeList.Attributes)
                .Any(attribute => attribute.Name.ToString() == "Conditional");
            if (hasConditionalAttribute)
                return;

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using BestPracticeChecker.Resources;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace BestPracticeChecker
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConditionalDebugAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ConditionalDebug";

        // You can change these strings in the Resources.resx file. On Mac you can use Rider to generate the corresponding code.
        // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Localizing%20Analyzers.md for more on localization

        private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Strings.ConditionalDebugTitle), Strings.ResourceManager, typeof(Strings));
        private static readonly LocalizableString MessageFormat = new LocalizableResourceString(nameof(Strings.ConditionalDebugMessageFormat), Strings.ResourceManager, typeof(Strings));
        private static readonly LocalizableString Description = new LocalizableResourceString(nameof(Strings.ConditionalDebugDescription), Strings.ResourceManager, typeof(Strings));
        private static readonly string Category = DiagnosticCategory.Correctness;

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            // See https://github.com/dotnet/roslyn/blob/master/docs/analyzers/Analyzer%20Actions%20Semantics.md for more information
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);

        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var namespaceName = methodSymbol.ContainingNamespace.Name;
            if (namespaceName != "UnityEngine")
                return;

            var typeName = methodSymbol.ContainingType.Name;
            if (typeName != "Debug")
                return;


            // Go on for multiple `Debug` methods with List and Equals
            var methodName = methodSymbol.Name;
            //if (methodName != "Log")
            //    return;

            List<string> logholder = new List<string>()
            {
                "Log",
                "LogAssertion",
                "LogError",
                "LogException",
                "LogWarning",
                "LogAssertionFormat",
                "LogErrorFormat",
                "LogFormat",
                "LogWarningFormat"
            };

            var isLogMethod = logholder.Exists(m => m.Equals(methodName));
            if (isLogMethod != true)
            {
                return;
            }

            var outerMethodDeclaration = invocationExpression.FirstAncestorOrSelf<MethodDeclarationSyntax>();
            if (outerMethodDeclaration == null)
                return;

            foreach (var attributeList in outerMethodDeclaration.AttributeLists)
            {
                foreach (var attribute in attributeList.Attributes)
                {
                    if (attribute.Name.ToString() == "Conditional")
                    {
                        return;
                    }
                }
            }

            context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}

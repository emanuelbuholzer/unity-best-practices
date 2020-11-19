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
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AllocatingPhysicApiAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0020",
                DiagnosticStrings.GetString(nameof(Strings.NonAllocatingPhysicEngineTitle)),
                DiagnosticStrings.GetString(nameof(Strings.NonAllocatingPhysicEngineMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Correctness,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0020_NonAllocatingPhysics.md"));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var methods = new List<Symbol>()
            {
                //Physics-Methods
                Symbol.From("UnityEngine", "Physics", "BoxCast"),
                Symbol.From("UnityEngine", "Physics", "CapsuleCast"),
                Symbol.From("UnityEngine", "Physics", "OverlapBox"),
                Symbol.From("UnityEngine", "Physics", "OverlapCapsule"),
                Symbol.From("UnityEngine", "Physics", "OverlapSphere"),
                Symbol.From("UnityEngine", "Physics", "Raycast"),
                Symbol.From("UnityEngine", "Physics", "SphereCast"),
                
                //Extensions Physics2D-Methods
                Symbol.From("UnityEngine", "Physics2D", "Boxcast"),
                Symbol.From("UnityEngine", "Physics2D", "CapsuleCast"),
                Symbol.From("UnityEngine", "Physics2D", "CircleCast"),
                Symbol.From("UnityEngine", "Physics2D", "GetRayIntersection"),
                Symbol.From("UnityEngine", "Physics2D", "Linecast"),
                Symbol.From("UnityEngine", "Physics2D", "OverlapArea"),
                Symbol.From("UnityEngine", "Physics2D", "OverlapBox"),
                Symbol.From("UnityEngine", "Physics2D", "OverlapCapsule"),
                Symbol.From("UnityEngine", "Physics2D", "OverlapCircle"),
                Symbol.From("UnityEngine", "Physics2D", "OverlapPoint"),
                Symbol.From("UnityEngine", "Physics2D", "Raycast"),
            };

            if (!methods.Any(m => m.Equals(methodSymbol)))
                return;
            
            context.ReportDiagnostic(Diagnostic.Create(Rule, invocationExpression.GetLocation()));
        }
    }
}

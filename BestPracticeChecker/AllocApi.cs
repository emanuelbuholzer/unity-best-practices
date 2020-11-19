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
    public class AllocApi : DiagnosticAnalyzer
    {

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0020",
                DiagnosticStrings.GetString(nameof(Strings.NonAllocatingPhysicEngineTitle)),
                DiagnosticStrings.GetString(nameof(Strings.NonAllocatingPhysicEngineMessageFormat)),
                DiagnosticStrings.DiagnosticCategory.Performance,
                DiagnosticSeverity.Warning,
                isEnabledByDefault: true,
                description: DiagnosticStrings.GetString(nameof(Strings.NonAllocatingPhysicEngineDescription)),
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0020_NonAllocatingPhysics.md"));

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;

            var method = Symbol.From(methodSymbol);

            var methodsArgumentExtractors = new HashSet<Tuple<Symbol, ArgumentExtractor>>()
            {
                //Physics-Methods
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "BoxCast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "CapsuleCast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                 Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "OverlapBox"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                  Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "OverlapCapsule"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "OverlapSphere"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                    Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "Raycast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                     Tuple.Create(
                    Symbol.From("UnityEngine", "Physics", "SphereCast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),

                //Extensions Physics2D-Methods
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "Boxcast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "CapsuleCast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "CircleCast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "GetRayIntersection"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                 Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "Linecast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                  Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "OverlapArea"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                  Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "OverlapBox"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                 Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "OverlapCapsule"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "OverlapCircle"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "OverlapPoint"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Physics2D", "Raycast"),
                    new ArgumentExtractor(arguments => ImmutableList.Create(arguments.First().Expression))
                    ),
            };

            var methodArgumentExtractor =
                methodsArgumentExtractors
                    .Where(m => m.Item1.Equals(method))
                    .Select(m => m.Item2)
                    .SingleOrDefault();
            if (methodArgumentExtractor == null)
                return;

            var argumentExpressions = methodArgumentExtractor.Invoke(invocationExpression.ArgumentList.Arguments);
            var isConstTagArguments = argumentExpressions
                .Select(a => context.SemanticModel.GetConstantValue(a, context.CancellationToken))
                .Select(r => r.HasValue);

            var missingConstTagArgumentExpressions = argumentExpressions
                .Zip(isConstTagArguments, Tuple.Create)
                .Where(t => !t.Item2)
                .Select(t => t.Item1);

            foreach (var argumentExpression in missingConstTagArgumentExpressions)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, argumentExpression.GetLocation()));
            }
        }
    }
}

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
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ConstTagsAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule =
            new DiagnosticDescriptor(
                "BP0004",
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.ConstTagsMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                helpLinkUri: DiagnosticStrings.GetHelpLinkUri("BP0004_ConstantStrings.md"));
        
        public override void Initialize(AnalysisContext context)
        {
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
                Tuple.Create(
                    Symbol.From("UnityEngine", "GameObject", "FindWithTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "GameObject", "FindGameObjectsWithTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Application", "SetBuildTags"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Material", "GetTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Component", "CompareTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "GameObject", "CompareTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.AnimationModule", "AnimatorStateInfo", "IsTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Material", "SetOverrideTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.iOS.Xcode", "PBXProject", "RemoveAssetTag"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.iOS.Xcode", "PBXProject", "AddAssetTagForFile"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(2)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.iOS.Xcode", "PBXProject", "AddAssetTagToDefaultInstall"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(1)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine", "Application", "SetBuildTags"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.iOS.Xcode", "PBXProject", "RemoveAssetTagForFile"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(2)
                ),
                Tuple.Create(
                    Symbol.From("UnityEngine.iOS.Xcode", "PBXProject", "RemoveAssetTagFromDefaultInstall"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(1)
                ),
                Tuple.Create(
                    Symbol.From("UnityEditor.Profiling", "HierarchyFrameDataView", "GetFrameMetaDataCount"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(1)
                ),
                Tuple.Create(
                    Symbol.From("UnityEditor.XR.Provider", "XRStats", "TryGetStat"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(1)
                ),
                Tuple.Create(
                    Symbol.From("UnityEditor.iOS", "OnDemandResources", "PreloadAsync"),
                    ArgumentExpressionExtractorFactory.CreatePositionalExtractor(0)
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
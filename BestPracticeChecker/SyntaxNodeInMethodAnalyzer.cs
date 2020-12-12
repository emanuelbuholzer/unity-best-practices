using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    using NodeWithSymbol = Tuple<SyntaxNode, Symbol>;
        
    public abstract class SyntaxNodeInMethodAnalyzer : DiagnosticAnalyzer
    {
        protected abstract SyntaxKind ByKind { get; }
        protected abstract ImmutableList<Symbol> InMethods { get; }
        protected abstract ImmutableList<Symbol> FilterBySymbols { get; }

        private readonly List<Symbol> _typeMethods = new List<Symbol>();
        private readonly List<Symbol> _baseTypeMethods = new List<Symbol>();
        private readonly List<List<Location>> _methodLocations = new List<List<Location>>();
        private readonly List<NodeWithSymbol> _methodInvocationsWithSymbols = new List<NodeWithSymbol>();
        private readonly List<NodeWithSymbol> _syntaxNodesWithSymbols = new List<NodeWithSymbol>();

        public sealed override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            context.RegisterSymbolAction(AnalyzeMethodSymbol, SymbolKind.Method);
            context.RegisterSyntaxNodeAction(AnalyzeInvocationNodes, SyntaxKind.InvocationExpression);
            context.RegisterSyntaxNodeAction(AnalyzeNodeByKind, ByKind);
            context.RegisterSemanticModelAction(AnalyzeModel);
        }

        public abstract void AnalyzeNodeInMethod(SemanticModelAnalysisContext context, SyntaxNode node);
        
        private void AnalyzeMethodSymbol(SymbolAnalysisContext context)
        {
            _typeMethods.Add(Symbol.From(context.Symbol));

            var baseType = context.Symbol.ContainingType.BaseType;
            _baseTypeMethods.Add(baseType != null
                ? Symbol.From(baseType.ContainingNamespace.Name, baseType.Name, context.Symbol.Name)
                : Symbol.Empty()
            );

            _methodLocations.Add(context.Symbol.Locations.ToList());
        }

        private void AnalyzeInvocationNodes(SyntaxNodeAnalysisContext context) =>
            _methodInvocationsWithSymbols.Add(
                new NodeWithSymbol(
                    context.Node as InvocationExpressionSyntax, 
                    Symbol.From((ISymbol) context.SemanticModel.GetSymbolInfo(context.Node).Symbol))
            );
        private void AnalyzeNodeByKind(SyntaxNodeAnalysisContext context) =>
            _syntaxNodesWithSymbols.Add(new NodeWithSymbol(
                context.Node, 
                Symbol.From((ISymbol) context.SemanticModel.GetSymbolInfo(context.Node).Symbol))
            );
        
        private void AnalyzeModel(SemanticModelAnalysisContext context)
        {
            var startingLocations = InMethods.SelectMany(GetMethodLocation);
            
            foreach (var node in FindSyntaxNodes(_methodInvocationsWithSymbols, _syntaxNodesWithSymbols, startingLocations))
            {
                AnalyzeNodeInMethod(context, node);
            }
        }

        private IEnumerable<SyntaxNode> FindSyntaxNodes(
            IEnumerable<NodeWithSymbol> methodInvocationsWithSymbols,
            IEnumerable<NodeWithSymbol> syntaxNodesWithSymbols,
            IEnumerable<Location> locations)
        {
            var methodSearch = NodeInMethodDeclarationSearch.Create();
            var isInLocation = new Func<NodeWithSymbol, bool>(t =>
                methodSearch.WithNode(t.Item1).WithMethodLocations(locations).Search().Any);

            var equalsAnyFilterSymbol = new Func<NodeWithSymbol, bool>(t =>
                FilterBySymbols.Any(s => s.Equals(t.Item2)));

            var notEqualsAnyFilterSymbol = new Func<NodeWithSymbol, bool>(t => !equalsAnyFilterSymbol(t));

            var nodesWithinLocation = 
                syntaxNodesWithSymbols.Where(isInLocation);

            var foundNodes = 
                nodesWithinLocation.Where(equalsAnyFilterSymbol).Select(t => t.Item1).ToList();

            var invocationsToSearch = methodInvocationsWithSymbols
                .Where(isInLocation)
                .Where(notEqualsAnyFilterSymbol)
                .SelectMany(t => GetMethodLocation(t.Item2));

            if (!invocationsToSearch.Any())
                return foundNodes;
            
            foundNodes.AddRange(
                FindSyntaxNodes(methodInvocationsWithSymbols, syntaxNodesWithSymbols, invocationsToSearch)
                );

            return foundNodes;
        }
        
        private IEnumerable<Location> GetMethodLocation(Symbol method)
        {
            var typeMethodLocations = _typeMethods
                .Zip(_methodLocations, Tuple.Create)
                .Where(t => method.Equals(t.Item1))
                .SelectMany(t => t.Item2);

            var baseTypeMethodLocations = _baseTypeMethods
                .Zip(_methodLocations, Tuple.Create)
                .Where(t => method.Equals(t.Item1))
                .SelectMany(t => t.Item2);

            var locations = new List<Location>();
            locations.AddRange(typeMethodLocations);
            locations.AddRange(baseTypeMethodLocations);
            return locations;
        }
    }
}
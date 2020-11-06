using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class IllegalMethodInUpdateAnalyzer : DiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);
        private static readonly DiagnosticDescriptor Rule = 
            new DiagnosticDescriptor(
                "BP0001", 
                DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateTitle)), 
                DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateMessageFormat)), 
                DiagnosticStrings.DiagnosticCategory.Performance, 
                DiagnosticSeverity.Warning, 
                isEnabledByDefault: true, 
                description: DiagnosticStrings.GetString(nameof(Strings.IllegalMethodInUpdateDescription)));

        private MethodExtractor _methodExtractor;
        private List<Tuple<Symbol, InvocationExpressionSyntax>> _invokedMethods = new List<Tuple<Symbol, InvocationExpressionSyntax>>();
        
        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.None);
            _methodExtractor = MethodExtractor.Register(context);
            context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
            context.RegisterSemanticModelAction(AnalyzeModel);
        }

        //private IEnumerable<InvocationExpressionSyntax> FindInvocationsInMethods(IEnumerable<Location> methodLocations,
        //    IEnumerable<Symbol> invokedMethods)
        //{
        //    
        //}

        private IEnumerable<InvocationExpressionSyntax> FindInvocationsRecursive(IEnumerable<Location> locations, int maxDepth)
        {
            if (maxDepth == 0)
            {
                return ImmutableList<InvocationExpressionSyntax>.Empty;
            }
            var searchResults = _invokedMethods
                .Select(i => (i.Item1, i.Item2, locations.Select(l => 
                        NodeInMethodSearch.Create().WithNode(i.Item2).WithMethodLocation(l).Search())))
                .Where(t => t.Item3.Any(s => s.InMethod))
                .Select(t => (t.Item1, t.Item2));
            
            var methods = new List<Symbol>
            {
                Symbol.From("UnityEngine", "GameObject", "GetComponent"),
                Symbol.From("UnityEngine", "GameObject", "GetComponents"),
                Symbol.From("UnityEngine", "Object", "FindObjectOfType"),
                Symbol.From("UnityEngine", "GameObject", "GetComponentInParent"),
                // TODO: Component.GetComponent?
            };

            var illno = searchResults
                .Where(s => !methods.Any(m => s.Item1.Equals(m)))
                .Select(t => t.Item2.GetLocation());

            var e = FindInvocationsRecursive(illno, maxDepth-1);

            var ill = searchResults
                .Where(s => !methods.Any(m => s.Item1.Equals(m)))
                .Select(t => t.Item2);
            
            var r = new List<InvocationExpressionSyntax>();
            r.AddRange(e);
            r.AddRange(ill);
            return r;



        }
        
        private void AnalyzeModel(SemanticModelAnalysisContext context)
        {
            var updateMethodLocations = _methodExtractor.GetLocationsByBaseTypeMethod(
                Symbol.From("UnityEngine", "MonoBehaviour", "Update")
            );

            var ill = FindInvocationsRecursive(updateMethodLocations, 3);

            //var searchResults = _invokedMethods
            //    .Select(i =>
            //    {
            //        var methodSearch = NodeInMethodSearch.Create().WithNode(i.Item2);
            //        return updateMethodLocations.Select(l =>
            //            methodSearch.WithMethodLocation(l).Search()
            //        );
            //    })
            //    .Zip(_invokedMethods, (a, b) => Tuple.Create(a, b.Item1, b.Item2))
            //    .Where(t => t.Item1.Any(s => s.InMethod))
            //    .Select(t => Tuple.Create(t.Item2, t.Item3));

            //
            //var methods = new List<Symbol>
            //{
            //    Symbol.From("UnityEngine", "GameObject", "GetComponent"),
            //    Symbol.From("UnityEngine", "GameObject", "GetComponents"),
            //    Symbol.From("UnityEngine", "Object", "FindObjectOfType"),
            //    Symbol.From("UnityEngine", "GameObject", "GetComponentInParent"),
            //    // TODO: Component.GetComponent?
            //};
            //var ill = searchResults.Where(s => methods.Any(m => s.Item1.Equals(m))).Select(t => t.Item2);
            //
            //var illno = searchResults.Where(s => !methods.Any(m => s.Item1.Equals(m)));
            //foreach (var syntax in illno)
            //{
            //    syntax.Item2.GetLocation()
            //}
            
            foreach (var syntax in ill)
            {
                context.ReportDiagnostic(Diagnostic.Create(Rule, syntax.GetLocation()));
            }


        }

        private void AnalyzeNode(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;
            var methodSymbol = context.SemanticModel.GetSymbolInfo(invocationExpression, context.CancellationToken).Symbol as IMethodSymbol;
            
            _invokedMethods.Add(Tuple.Create(Symbol.From(methodSymbol), invocationExpression));

            //var inMethodSearch = NodeInMethodSearch.Create().WithNode(invocationExpression);
            //var updateMethodLocations = _methodExtractor.GetLocationsByBaseTypeMethod(
            //    Symbol.From("UnityEngine", "MonoBehaviour", "Update")
            //);
            //if (!updateMethodLocations.Any(l => inMethodSearch.WithMethodLocation(l).Search().InMethod))
            //{
            //    return;
            //}

            //var method = Symbol.From(methodSymbol);
            //var methods = new List<Symbol>
            //{
            //    Symbol.From("UnityEngine", "GameObject", "GetComponent"),
            //    Symbol.From("UnityEngine", "GameObject", "GetComponents"),
            //    Symbol.From("UnityEngine", "Object", "FindObjectOfType"),
            //    Symbol.From("UnityEngine", "GameObject", "GetComponentInParent"),
            //    // TODO: Component.GetComponent?
            //}; 
            //if (!methods.Any(m => method.Equals(m)))
            //    return;
           
            //

            //context.ReportDiagnostic(Diagnostic.Create(Rule, context.Node.GetLocation()));
        }
    }
}
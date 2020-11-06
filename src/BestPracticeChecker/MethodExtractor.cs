using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Schema;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace BestPracticeChecker
{
    public class MethodExtractor
    {
        private List<Symbol> _methods = new List<Symbol>();
        private List<Symbol> _baseTypes = new List<Symbol>();
        private List<List<Location>> _locations = new List<List<Location>>();
        
        private MethodExtractor() { }

        public static MethodExtractor Register(AnalysisContext context)
        {
            var updateMethodLocationSymbolExtractor = new MethodExtractor();
            context.RegisterSymbolAction(updateMethodLocationSymbolExtractor.AnalyzeSymbol, SymbolKind.Method);
            return updateMethodLocationSymbolExtractor;
        }

        private void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            var symbol = context.Symbol;
        
            _methods.Add(Symbol.From(symbol));
            var baseType = symbol.ContainingType.BaseType;
            _baseTypes.Add(baseType != null
                ? Symbol.From(baseType.ContainingNamespace.Name, baseType.Name, symbol.Name)
                : Symbol.Empty());

            _locations.Add(symbol.Locations.ToList());
        }

        public IEnumerable<Location> GetLocationsByMethod(Symbol method)
        {
            return _methods
                .Zip(_locations, Tuple.Create)
                .Where(m => method.Equals(m.Item1))
                .SelectMany(m => m.Item2);
        }

        public IEnumerable<Location> GetLocationsByBaseTypeMethod(Symbol method)
        {
            return _baseTypes
                .Zip(_locations, Tuple.Create)
                .Where(m => method.Equals(m.Item1))
                .SelectMany(m => m.Item2); 
        }
        
        public IEnumerable<Symbol> GetMethodSymbols()
        {
            return _methods;
        }
    }
}
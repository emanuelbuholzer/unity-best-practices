using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.CodeAnalysis;

namespace BestPracticeChecker
{
    public class MethodSymbolMatcher
    {
        private readonly IMethodSymbol _methodSymbol;
        
        private MethodSymbolMatcher(IMethodSymbol methodSymbol)
        {
            _methodSymbol = methodSymbol;
        }

        public static MethodSymbolMatcher FromMethodSymbol(IMethodSymbol methodSymbol)
        {
            if (methodSymbol == null)
                throw new ArgumentNullException(nameof(methodSymbol));
            
            return new MethodSymbolMatcher(methodSymbol);
        }

        public bool MatchNamespace(string matchingNamespace)
        {
            return _methodSymbol.ContainingNamespace.Name.Equals(matchingNamespace);
        }

        public bool MatchType(string matchingNamespace, string matchingType)
        {
            return MatchNamespace(matchingNamespace) && _methodSymbol.ContainingType.Name.Equals(matchingType);
        }

        public bool MatchMethod(string matchingNamespace, string matchingType, string matchingMethod)
        {
            return MatchType(matchingNamespace, matchingType) && _methodSymbol.Name.Equals(matchingMethod);
        }
    }
}
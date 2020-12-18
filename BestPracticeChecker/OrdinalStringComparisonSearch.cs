using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BestPracticeChecker
{
    public readonly struct OrdinalStringComparisonSearchResult
    {
        public OrdinalStringComparisonSearchResult(bool any)
        {
            Any = any;
        }

        public bool Any { get; }
    }
    
    public class OrdinalStringComparisonSearch
    {
        private ArgumentListSyntax _arguments;
        
        OrdinalStringComparisonSearch() { }
        public static OrdinalStringComparisonSearch Create() =>
            new OrdinalStringComparisonSearch();

        public OrdinalStringComparisonSearch WithArgumentList(ArgumentListSyntax arguments)
        {
            _arguments = arguments;
            return this;
        }

        public OrdinalStringComparisonSearchResult Search()
        {
            var allowedComparisons = new List<string>()
            {
                "StringComparison.Ordinal",
                "StringComparison.OrdinalIgnoreCase"
            };

            var arguments = _arguments.Arguments;
            var any = allowedComparisons.Any(a =>
                arguments[arguments.Count - 1].Expression.GetText().ToString().Equals(a));
            
            return new OrdinalStringComparisonSearchResult(any);
        }
    }
}
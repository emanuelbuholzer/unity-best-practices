using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BestPracticeChecker
{
    using ArgumentExtractor = Func<SeparatedSyntaxList<ArgumentSyntax>, IEnumerable<ExpressionSyntax>>;
    
    public static class ArgumentExpressionExtractorFactory
    {
        public static ArgumentExtractor CreatePositionalExtractor(params int[] positions)
        {
            return new ArgumentExtractor(arguments => positions.Select(position => arguments[position].Expression));
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace BestPracticeChecker
{
    public readonly struct NodeInMethodSearchResult
    {
        public NodeInMethodSearchResult(bool inMethod)
        {
            InMethod = inMethod;
        }

        public bool InMethod { get; }
    }
    
    public class NodeInMethodSearch
    {
        private Location _location;
        private IEnumerable<TextSpan> ancestorSourceSpans;

        private NodeInMethodSearch() { }

        public static NodeInMethodSearch Create()
        {
            return new NodeInMethodSearch();
        } 

        public NodeInMethodSearch WithMethodLocation(Location methodLocation)
        {
            _location = methodLocation;
            return this;
        }

        public NodeInMethodSearch WithNode(SyntaxNode node)
        {
            ancestorSourceSpans =
                node.AncestorsAndSelf()
                    .Where(n => n.Kind() == SyntaxKind.MethodDeclaration)
                    .Select(n => n.GetLocation().SourceSpan);
            return this;
        }

        public NodeInMethodSearchResult Search()
        {
            var inMethod = ancestorSourceSpans.Any(l => l.Contains(_location.SourceSpan));
            return new NodeInMethodSearchResult(inMethod);
        }
    }
}
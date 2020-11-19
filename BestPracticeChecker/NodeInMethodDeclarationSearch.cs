using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace BestPracticeChecker
{
    public readonly struct NodeInMethodDeclarationSearchResult
    {
        public NodeInMethodDeclarationSearchResult(bool any)
        {
            Any = any;
        }

        public bool Any { get; }
    }
    
    public class NodeInMethodDeclarationSearch
    {
        private IEnumerable<Location> _locations;
        private IEnumerable<TextSpan> ancestorSourceSpans;

        private NodeInMethodDeclarationSearch() { }

        public static NodeInMethodDeclarationSearch Create()
        {
            return new NodeInMethodDeclarationSearch();
        } 

        public NodeInMethodDeclarationSearch WithMethodLocation(Location methodLocation)
        {
            _locations = ImmutableList.Create(methodLocation);
            return this;
        }

        public NodeInMethodDeclarationSearch WithMethodLocations(IEnumerable<Location> methodLocations)
        {
            _locations = methodLocations;
            return this;
        }
        
        public NodeInMethodDeclarationSearch WithNode(SyntaxNode node)
        {
            ancestorSourceSpans =
                node.AncestorsAndSelf()
                    .Where(n => n.Kind() == SyntaxKind.MethodDeclaration)
                    .Select(n => n.GetLocation().SourceSpan);
            return this;
        }

        public NodeInMethodDeclarationSearchResult Search()
        {
            var inMethod = ancestorSourceSpans.Any(l => 
                _locations.Any(location => l.Contains(location.SourceSpan) && !l.Equals(location.SourceSpan))
            );
            return new NodeInMethodDeclarationSearchResult(inMethod);
        }
    }
}
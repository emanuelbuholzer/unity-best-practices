using System;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace BestPracticeChecker
{
    public readonly struct Symbol : IEquatable<Symbol>
    {
        private readonly string _namespaceIdentifier;
        private readonly string _typeIdentifier;
        private readonly string _nameIdentifier;

        private Symbol(string namespaceIdentifier, string typeIdentifier, string nameIdentifier)
        {
            _namespaceIdentifier = namespaceIdentifier;
            _typeIdentifier = typeIdentifier;
            _nameIdentifier = nameIdentifier;
        }

        public string NamespaceIdentifier => _namespaceIdentifier;

        public string TypeIdentifier => _typeIdentifier;

        public string NameIdentifier => _nameIdentifier;

        public static Symbol Empty()
        {
            return From("", "", "");
        }
        
        public static Symbol From(string namespaceIdentifier, string typeIdentifier, string nameIdentifier)
        {
            return new Symbol(namespaceIdentifier, typeIdentifier, nameIdentifier);
        }

        public static Symbol From(ISymbol symbol)
        {
            return From(symbol.ContainingNamespace.Name, symbol.ContainingType.Name, symbol.Name);
        }

        public bool Equals(string matchingNamespace, string matchingType, string matchingName)
        {
            return NamespaceIdentifier.Equals(matchingNamespace) && TypeIdentifier.Equals(matchingType) &&
                   NameIdentifier.Equals(matchingName);
        }

        public bool Equals(ISymbol symbol)
        {
            return Equals(symbol.ContainingNamespace.Name, symbol.ContainingType.Name, symbol.Name);
        }

        public bool Equals(Symbol other)
        {
            return NamespaceIdentifier == other.NamespaceIdentifier && TypeIdentifier == other.TypeIdentifier && NameIdentifier == other.NameIdentifier;
        }

        public override bool Equals(object obj)
        {
            return obj is Symbol other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (NamespaceIdentifier != null ? NamespaceIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (TypeIdentifier != null ? TypeIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (NameIdentifier != null ? NameIdentifier.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
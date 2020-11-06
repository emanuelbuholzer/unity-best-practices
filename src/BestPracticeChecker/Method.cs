using System;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace BestPracticeChecker
{
    public struct Method : IEquatable<Method>
    {
        internal readonly string _namespaceIdentifier;
        internal readonly string _typeIdentifier;
        internal readonly string _nameIdentifier;

        private Method(string namespaceIdentifier, string typeIdentifier, string nameIdentifier)
        {
            _namespaceIdentifier = namespaceIdentifier;
            _typeIdentifier = typeIdentifier;
            _nameIdentifier = nameIdentifier;
        }

        public static Method From(string namespaceIdentifier, string typeIdentifier, string nameIdentifier)
        {
            return new Method(namespaceIdentifier, typeIdentifier, nameIdentifier);
        }

        public static Method From(IMethodSymbol methodSymbol)
        {
            return From(methodSymbol.ContainingNamespace.Name, methodSymbol.ContainingType.Name, methodSymbol.Name);
        }

        public bool Equals(string matchingNamespace, string matchingType, string matchingName)
        {
            return _namespaceIdentifier.Equals(matchingNamespace) && _typeIdentifier.Equals(matchingType) &&
                   _nameIdentifier.Equals(matchingName);
        }

        public bool Equals(IMethodSymbol methodSymbol)
        {
            return Equals(methodSymbol.ContainingNamespace.Name, methodSymbol.ContainingType.Name, methodSymbol.Name);
        }

        public bool Equals(Method other)
        {
            return _namespaceIdentifier == other._namespaceIdentifier && _typeIdentifier == other._typeIdentifier && _nameIdentifier == other._nameIdentifier;
        }

        public override bool Equals(object obj)
        {
            return obj is Method other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_namespaceIdentifier != null ? _namespaceIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_typeIdentifier != null ? _typeIdentifier.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (_nameIdentifier != null ? _nameIdentifier.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
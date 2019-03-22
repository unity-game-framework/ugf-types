using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public interface ITypeProvider<TIdentifier> : ITypeProvider, IEnumerable<KeyValuePair<TIdentifier, Type>>
    {
        IEqualityComparer<TIdentifier> Comparer { get; }

        bool Contains(TIdentifier identifier);
        bool Contains(Type type);
        void Add(TIdentifier identifier, Type type);
        bool Remove(TIdentifier identifier);
        void Clear();
        Type Get(TIdentifier identifier);
        bool TryGet(TIdentifier identifier, out Type type);
        TIdentifier GetIdentifier(Type type);
        bool TryGetIdentifier(Type type, out TIdentifier identifier);
    }
}
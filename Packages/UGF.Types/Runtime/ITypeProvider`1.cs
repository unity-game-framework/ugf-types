using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents the type provider that store type by specific identifier.
    /// </summary>
    public interface ITypeProvider<TIdentifier>
    {
        /// <summary>
        /// Gets the type of identifier used to store types.
        /// </summary>
        Type IdentifierType { get; }

        IEqualityComparer<TIdentifier> IdentifierComparer { get; }

        IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        void Add(TIdentifier identifier, Type type);

        bool Remove(TIdentifier identifier);

        void Clear();

        TIdentifier GetIdentifier(Type type);

        bool TryGetIdentifier(Type type, out TIdentifier identifier);
    }
}

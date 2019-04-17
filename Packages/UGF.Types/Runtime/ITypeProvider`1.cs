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

        /// <summary>
        /// Gets the comparer of the identifiers.
        /// </summary>
        IEqualityComparer<TIdentifier> IdentifierComparer { get; }

        /// <summary>
        /// Gets the collection of the types and their identifiers.
        /// </summary>
        IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        /// <summary>
        /// Tries to add specified type, if that contains attribute with supported identifier type.
        /// </summary>
        /// <param name="type">The type to add.</param>
        bool TryAdd(Type type);

        /// <summary>
        /// Adds the specified type by specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        /// <param name="type">The type to add.</param>
        void Add(TIdentifier identifier, Type type);

        /// <summary>
        /// Removes type by specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type to remove.</param>
        bool Remove(TIdentifier identifier);

        /// <summary>
        /// Clears provider from all types.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets identifier by the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        TIdentifier GetIdentifier(Type type);

        /// <summary>
        /// Tries to get identifier by the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        /// <param name="identifier">The found identifier.</param>
        bool TryGetIdentifier(Type type, out TIdentifier identifier);
    }
}

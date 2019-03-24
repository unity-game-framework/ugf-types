using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents the type provider that store type by specific identifier.
    /// </summary>
    public interface ITypeProvider<TIdentifier> : ITypeProvider, IEnumerable<KeyValuePair<TIdentifier, Type>>
    {
        /// <summary>
        /// Gets the identifier comparer.
        /// </summary>
        IEqualityComparer<TIdentifier> Comparer { get; }

        /// <summary>
        /// Determines whether provider contains type with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type to check.</param>
        bool Contains(TIdentifier identifier);

        /// <summary>
        /// Determines whether provider contains the specified type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        bool Contains(Type type);

        /// <summary>
        /// Adds the specified type by the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        /// <param name="type">The type to add.</param>
        void Add(TIdentifier identifier, Type type);

        /// <summary>
        /// Removes type with the specified identifier, if presents.
        /// </summary>
        /// <param name="identifier">The identifier of the type to remove.</param>
        bool Remove(TIdentifier identifier);

        /// <summary>
        /// Removes the specified type from the provider, if presents.
        /// </summary>
        /// <param name="type">The type to remove.</param>
        bool Remove(Type type);

        /// <summary>
        /// Clears provider from all types.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets type with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        Type Get(TIdentifier identifier);

        /// <summary>
        /// Tries to get type with the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        /// <param name="type">The found type.</param>
        bool TryGet(TIdentifier identifier, out Type type);

        /// <summary>
        /// Gets identifier of the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        TIdentifier GetIdentifier(Type type);

        /// <summary>
        /// Tries to get identifier of the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        /// <param name="identifier">The found identifier.</param>
        bool TryGetIdentifier(Type type, out TIdentifier identifier);
    }
}
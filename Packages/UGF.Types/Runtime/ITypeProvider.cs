using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents the type provider that store type by specific identifier.
    /// </summary>
    public interface ITypeProvider
    {
        /// <summary>
        /// Gets the type of identifier used to store types.
        /// </summary>
        Type IdentifierType { get; }

        /// <summary>
        /// Gets the collection of the types and their identifiers.
        /// </summary>
        IEnumerable<KeyValuePair<object, Type>> Types { get; }

        /// <summary>
        /// Adds the specified type by specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        /// <param name="type">The type to add.</param>
        void Add(object identifier, Type type);

        /// <summary>
        /// Removes type by specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type to remove.</param>
        bool Remove(object identifier);

        /// <summary>
        /// Clears provider from all types.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets identifier by the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        object GetIdentifier(Type type);

        /// <summary>
        /// Tries to get identifier by the specified type.
        /// </summary>
        /// <param name="type">The type to get identifier of.</param>
        /// <param name="identifier">The found identifier.</param>
        bool TryGetIdentifier(Type type, out object identifier);
    }
}

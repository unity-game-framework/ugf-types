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

        IEnumerable<KeyValuePair<object, Type>> Types { get; }

        void Add(object identifier, Type type);

        bool Remove(object identifier);

        void Clear();

        object GetIdentifier(Type type);

        bool TryGetIdentifier(Type type, out object identifier);
    }
}

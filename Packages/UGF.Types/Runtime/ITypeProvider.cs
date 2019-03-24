using System;
using System.Collections;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents the type provider that store type by specific identifier.
    /// </summary>
    public interface ITypeProvider : IEnumerable
    {
        /// <summary>
        /// Gets count of the types in provider.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Gets the type of identifier used to store types.
        /// </summary>
        Type IdentifierType { get; }
    }
}
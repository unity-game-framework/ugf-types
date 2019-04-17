using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents types definition for external types.
    /// </summary>
    public interface ITypeDefine<TIdentifier>
    {
        /// <summary>
        /// Gets the type of identifier used to register in provider.
        /// </summary>
        Type IdentifierType { get; }

        /// <summary>
        /// Gets the collection of the identifiers and types.
        /// </summary>
        IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        /// <summary>
        /// Registers types into the specified provider.
        /// </summary>
        /// <param name="provider">The type provider to register types into.</param>
        void Register(ITypeProvider<TIdentifier> provider);

        /// <summary>
        /// Unregisters types from the specified provider.
        /// </summary>
        /// <param name="provider">The type provider to remove types from.</param>
        void Unregister(ITypeProvider<TIdentifier> provider);
    }
}

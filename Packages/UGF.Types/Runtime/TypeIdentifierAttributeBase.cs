using System;
using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents abstract base browsable attribute that store identifier of the type.
    /// </summary>
    public abstract class TypeIdentifierAttributeBase : AssemblyBrowsableTypeAttribute
    {
        /// <summary>
        /// Gets the type of identifier.
        /// </summary>
        public abstract Type IdentifierType { get; }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        public abstract object GetIdentifier();
    }
}

using System;
using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents browsable attribute that store identifier of the type.
    /// </summary>
    public class TypeIdentifierAttribute : AssemblyBrowsableTypeAttribute
    {
        /// <summary>
        /// Gets the type of identifier.
        /// </summary>
        public Type IdentifierType { get; }

        private readonly object m_identifier;

        public TypeIdentifierAttribute(Type identifierType, object identifier)
        {
            m_identifier = identifier;
            IdentifierType = identifierType;
        }

        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        public object GetIdentifier()
        {
            return m_identifier;
        }
    }
}

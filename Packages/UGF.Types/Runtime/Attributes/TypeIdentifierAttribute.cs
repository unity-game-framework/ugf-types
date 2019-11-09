using System;

namespace UGF.Types.Runtime.Attributes
{
    /// <summary>
    /// Represents browsable attribute that store identifier of the type.
    /// </summary>
    public class TypeIdentifierAttribute : Attribute
    {
        /// <summary>
        /// Gets the type of identifier.
        /// </summary>
        public Type IdentifierType { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public object Identifier { get; }

        /// <summary>
        /// Creates with the specified type of the identifier and identifier value.
        /// </summary>
        /// <param name="identifierType">The type of the identifier.</param>
        /// <param name="identifier">The identifier value.</param>
        public TypeIdentifierAttribute(Type identifierType, object identifier)
        {
            IdentifierType = identifierType ?? throw new ArgumentNullException(nameof(identifierType));
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
        }
    }
}

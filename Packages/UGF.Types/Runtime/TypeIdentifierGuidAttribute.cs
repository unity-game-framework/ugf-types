using System;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents attribute to store Guid identifier of the target type.
    /// </summary>
    public sealed class TypeIdentifierGuidAttribute : TypeIdentifierAttribute, ITypeIdentifierAttribute<Guid>
    {
        public Guid Identifier { get; }

        /// <summary>
        /// Creates attribute with the specified string representation of the guid.
        /// </summary>
        /// <param name="identifier">The string representation of the guid.</param>
        public TypeIdentifierGuidAttribute(string identifier) : base(typeof(Guid), new Guid(identifier))
        {
            Identifier = new Guid(identifier);
        }

        /// <summary>
        /// Creates attribute with the specified guid.
        /// </summary>
        /// <param name="identifier">The guid identifier.</param>
        public TypeIdentifierGuidAttribute(Guid identifier) : base(typeof(Guid), identifier)
        {
            Identifier = identifier;
        }
    }
}

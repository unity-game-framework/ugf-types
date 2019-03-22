using System;

namespace UGF.Types.Runtime
{
    public sealed class TypeIdentifierGuidAttribute : TypeIdentifierAttributeBase, ITypeIdentifierAttribute<Guid>
    {
        public Guid Identifier { get; }

        public TypeIdentifierGuidAttribute(string identifier)
        {
            Identifier = new Guid(identifier);
        }

        public TypeIdentifierGuidAttribute(Guid identifier)
        {
            Identifier = identifier;
        }

        public override object GetIdentifier()
        {
            return Identifier;
        }
    }
}
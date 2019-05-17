using System;

namespace UGF.Types.Runtime
{
    public class TypeIdentifierInt32Attribute : TypeIdentifierAttribute, ITypeIdentifierAttribute<int>
    {
        public int Identifier { get; }

        public TypeIdentifierInt32Attribute(int identifier) : base(typeof(int), identifier)
        {
            Identifier = identifier;
        }
    }
}

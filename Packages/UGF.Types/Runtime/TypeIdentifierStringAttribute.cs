namespace UGF.Types.Runtime
{
    public class TypeIdentifierStringAttribute : TypeIdentifierAttribute, ITypeIdentifierAttribute<string>
    {
        public string Identifier { get; }

        public TypeIdentifierStringAttribute(string identifier) : base(typeof(string), identifier)
        {
            Identifier = identifier;
        }
    }
}

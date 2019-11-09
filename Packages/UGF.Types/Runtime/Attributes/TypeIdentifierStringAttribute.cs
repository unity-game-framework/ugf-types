namespace UGF.Types.Runtime.Attributes
{
    /// <summary>
    /// Presents attribute to store identifier as String for the target type.
    /// </summary>
    public sealed class TypeIdentifierStringAttribute : TypeIdentifierAttribute, ITypeIdentifierAttribute<string>
    {
        public new string Identifier { get; }

        /// <summary>
        /// Creates attribute with the specified identifier as String.
        /// </summary>
        /// <param name="identifier">The identifier value.</param>
        public TypeIdentifierStringAttribute(string identifier) : base(typeof(string), identifier)
        {
            Identifier = identifier;
        }
    }
}

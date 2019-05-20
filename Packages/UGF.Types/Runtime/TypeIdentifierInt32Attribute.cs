namespace UGF.Types.Runtime
{
    /// <summary>
    /// Presents attribute to store identifier as Int32 for the target type.
    /// </summary>
    public sealed class TypeIdentifierInt32Attribute : TypeIdentifierAttribute, ITypeIdentifierAttribute<int>
    {
        public int Identifier { get; }

        /// <summary>
        /// Creates attribute with the specified identifier as Int32.
        /// </summary>
        /// <param name="identifier">The identifier value.</param>
        public TypeIdentifierInt32Attribute(int identifier) : base(typeof(int), identifier)
        {
            Identifier = identifier;
        }
    }
}

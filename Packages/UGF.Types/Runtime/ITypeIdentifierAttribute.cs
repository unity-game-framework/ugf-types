namespace UGF.Types.Runtime
{
    /// <summary>
    /// The generic interface to define the type of identifier for attribute.
    /// </summary>
    public interface ITypeIdentifierAttribute<out TIdentifier>
    {
        /// <summary>
        /// Gets the identifier value.
        /// </summary>
        TIdentifier Identifier { get; }
    }
}
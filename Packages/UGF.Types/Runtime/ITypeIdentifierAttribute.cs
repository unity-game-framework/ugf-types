namespace UGF.Types.Runtime
{
    public interface ITypeIdentifierAttribute<out TIdentifier>
    {
        TIdentifier Identifier { get; }
    }
}
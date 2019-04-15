namespace UGF.Types.Runtime
{
    public interface ITypeDefine<TIdentifier>
    {
        void Register(ITypeProvider<TIdentifier> provider);
        void Unregister(ITypeProvider<TIdentifier> provider);
    }
}

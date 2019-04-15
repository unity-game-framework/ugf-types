namespace UGF.Types.Runtime
{
    public interface ITypeDefine
    {
        void Register(ITypeProvider provider);
        void Unregister(ITypeProvider provider);
    }
}

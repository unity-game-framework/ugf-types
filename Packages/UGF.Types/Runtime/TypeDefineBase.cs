using System;

namespace UGF.Types.Runtime
{
    public abstract class TypeDefineBase<TIdentifier> : ITypeDefine<TIdentifier>, ITypeDefine
    {
        public abstract void Register(ITypeProvider<TIdentifier> provider);
        public abstract void Unregister(ITypeProvider<TIdentifier> provider);

        void ITypeDefine.Register(ITypeProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            Register((ITypeProvider<TIdentifier>)provider);
        }

        void ITypeDefine.Unregister(ITypeProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            Unregister((ITypeProvider<TIdentifier>)provider);
        }
    }
}

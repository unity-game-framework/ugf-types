using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public abstract class TypeDefineBase<TIdentifier> : ITypeDefine<TIdentifier>, ITypeDefine
    {
        public Type IdentifierType { get; } = typeof(TIdentifier);
        public abstract IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        IEnumerable<KeyValuePair<object, Type>> ITypeDefine.Types { get { return TypesEnumerable(); } }

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

        private IEnumerable<KeyValuePair<object, Type>> TypesEnumerable()
        {
            foreach (KeyValuePair<TIdentifier, Type> pair in Types)
            {
                yield return new KeyValuePair<object, Type>(pair.Key, pair.Value);
            }
        }
    }
}

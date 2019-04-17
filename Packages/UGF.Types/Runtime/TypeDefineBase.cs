using System;
using System.Collections.Generic;
using System.Linq;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents an abstract implementation of the definition for external types.
    /// </summary>
    public abstract class TypeDefineBase<TIdentifier> : ITypeDefine<TIdentifier>, ITypeDefine
    {
        public Type IdentifierType { get; } = typeof(TIdentifier);
        public abstract IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        IEnumerable<KeyValuePair<object, Type>> ITypeDefine.Types { get { return Types.Select(x => new KeyValuePair<object, Type>(x.Key, x.Value)); } }

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

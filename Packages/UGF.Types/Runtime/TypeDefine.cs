using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public class TypeDefine<T> : TypeDefineBase<T>
    {
        public Dictionary<T, Type> Types { get; } = new Dictionary<T, Type>();

        public override void Register(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (KeyValuePair<T, Type> pair in Types)
            {
                provider.Add(pair.Key, pair.Value);
            }
        }

        public override void Unregister(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (KeyValuePair<T, Type> pair in Types)
            {
                provider.Remove(pair.Key);
            }
        }
    }
}

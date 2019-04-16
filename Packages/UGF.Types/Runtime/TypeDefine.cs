using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Types.Runtime
{
    public class TypeDefine<T> : TypeDefineBase<T>
    {
        public override IReadOnlyDictionary<T, Type> Types { get; }

        private readonly Dictionary<T, Type> m_types = new Dictionary<T, Type>();

        public TypeDefine()
        {
            Types = new ReadOnlyDictionary<T, Type>(m_types);
        }

        protected void Add(T identifier, Type type)
        {
            m_types.Add(identifier, type);
        }

        protected void Remove(T identifier)
        {
            m_types.Remove(identifier);
        }

        public override void Register(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (KeyValuePair<T, Type> pair in m_types)
            {
                provider.Add(pair.Key, pair.Value);
            }
        }

        public override void Unregister(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (KeyValuePair<T, Type> pair in m_types)
            {
                provider.Remove(pair.Key);
            }
        }

        public Dictionary<T, Type>.Enumerator GetEnumerator()
        {
            return m_types.GetEnumerator();
        }
    }
}

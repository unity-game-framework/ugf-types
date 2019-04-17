using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents a default definition for external types.
    /// </summary>
    public class TypeDefine<T> : TypeDefineBase<T>
    {
        public override IReadOnlyDictionary<T, Type> Types { get; }

        private readonly Dictionary<T, Type> m_types = new Dictionary<T, Type>();

        /// <summary>
        /// Creates empty definition for external types.
        /// </summary>
        public TypeDefine()
        {
            Types = new ReadOnlyDictionary<T, Type>(m_types);
        }

        /// <summary>
        /// Adds specified type by the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type.</param>
        /// <param name="type">The type to add.</param>
        public void Add(T identifier, Type type)
        {
            m_types.Add(identifier, type);
        }

        /// <summary>
        /// Removes type from definition by the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier of the type to remove.</param>
        public bool Remove(T identifier)
        {
            return m_types.Remove(identifier);
        }

        /// <summary>
        /// Clears all types from definition.
        /// </summary>
        public void Clear()
        {
            m_types.Clear();
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

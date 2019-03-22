using System;
using System.Collections;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public class TypeProvider<TIdentifier> : ITypeProvider<TIdentifier>
    {
        public int Count { get { return m_types.Count; } }
        public Type IdentifierType { get; } = typeof(TIdentifier);
        public IEqualityComparer<TIdentifier> Comparer { get { return m_types.Comparer; } }

        private readonly Dictionary<TIdentifier, Type> m_types;
        private readonly Dictionary<Type, TIdentifier> m_identifiers;

        public TypeProvider(int capacity = 0, IEqualityComparer<TIdentifier> comparer = null)
        {
            if (capacity < 0) throw new ArgumentException("Capacity can not be less than zero.");
            
            m_types = new Dictionary<TIdentifier, Type>(capacity, comparer);
            m_identifiers = new Dictionary<Type, TIdentifier>(capacity);
        }

        public bool Contains(TIdentifier identifier)
        {
            return m_types.ContainsKey(identifier);
        }

        public bool Contains(Type type)
        {
            return m_identifiers.ContainsKey(type);
        }

        public void Add(TIdentifier identifier, Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            m_types.Add(identifier, type);
            m_identifiers.Add(type, identifier);
        }

        public bool Remove(TIdentifier identifier)
        {
            if (m_types.TryGetValue(identifier, out Type type))
            {
                m_types.Remove(identifier);
                m_identifiers.Remove(type);
                
                return true;
            }
            
            return false;
        }

        public void Clear()
        {
            m_types.Clear();
            m_identifiers.Clear();
        }

        public Type Get(TIdentifier identifier)
        {
            return m_types[identifier];
        }

        public bool TryGet(TIdentifier identifier, out Type type)
        {
            return m_types.TryGetValue(identifier, out type);
        }

        public TIdentifier GetIdentifier(Type type)
        {
            return m_identifiers[type];
        }

        public bool TryGetIdentifier(Type type, out TIdentifier identifier)
        {
            return m_identifiers.TryGetValue(type, out identifier);
        }

        public Dictionary<TIdentifier, Type>.Enumerator GetEnumerator()
        {
            return m_types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)m_types).GetEnumerator();
        }

        IEnumerator<KeyValuePair<TIdentifier, Type>> IEnumerable<KeyValuePair<TIdentifier, Type>>.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TIdentifier, Type>>)m_types).GetEnumerator();
        }
    }
}
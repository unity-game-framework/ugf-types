using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents the type provider that store type by specific identifier.
    /// </summary>
    public class TypeProvider<TIdentifier> : ITypeProvider<TIdentifier>, ITypeProvider
    {
        public Type IdentifierType { get; } = typeof(TIdentifier);
        public IEqualityComparer<TIdentifier> IdentifierComparer { get { return m_types.Comparer; } }
        public IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        IEnumerable<KeyValuePair<object, Type>> ITypeProvider.Types { get { return TypesEnumerable(); } }

        private readonly Dictionary<TIdentifier, Type> m_types;
        private readonly Dictionary<Type, TIdentifier> m_identifiers;

        /// <summary>
        /// Creates provider with specified capacity and identifier comparer.
        /// </summary>
        /// <param name="capacity">The initial capacity to store.</param>
        /// <param name="identifierComparer">The comparer of the identifiers.</param>
        public TypeProvider(int capacity = 0, IEqualityComparer<TIdentifier> identifierComparer = null)
        {
            if (capacity < 0) throw new ArgumentException("Capacity can not be less than zero.", nameof(capacity));

            m_types = new Dictionary<TIdentifier, Type>(capacity, identifierComparer);
            m_identifiers = new Dictionary<Type, TIdentifier>(capacity);

            Types = new ReadOnlyDictionary<TIdentifier, Type>(m_types);
        }

        public bool TryAdd(Type type)
        {
            if (TypesUtility.TryGetIdentifierFromType(type, out TIdentifier identifier))
            {
                Add(identifier, type);
                return true;
            }

            return false;
        }

        public void Add(TIdentifier identifier, Type type)
        {
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

        void ITypeProvider.Add(object identifier, Type type)
        {
            Add((TIdentifier)identifier, type);
        }

        bool ITypeProvider.Remove(object identifier)
        {
            return Remove((TIdentifier)identifier);
        }

        object ITypeProvider.GetIdentifier(Type type)
        {
            return GetIdentifier(type);
        }

        bool ITypeProvider.TryGetIdentifier(Type type, out object identifier)
        {
            if (TryGetIdentifier(type, out TIdentifier typeIdentifier))
            {
                identifier = typeIdentifier;
                return true;
            }

            identifier = null;
            return false;
        }

        private IEnumerable<KeyValuePair<object, Type>> TypesEnumerable()
        {
            foreach (KeyValuePair<TIdentifier, Type> pair in m_types)
            {
                yield return new KeyValuePair<object, Type>(pair.Key, pair.Value);
            }
        }
    }
}

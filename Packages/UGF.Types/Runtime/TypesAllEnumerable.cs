using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents enumerable through the all loaded types from the specified assembly collection.
    /// </summary>
    public struct TypesAllEnumerable : IEnumerable<Type>
    {
        private readonly Assembly m_assembly;
        private readonly IReadOnlyList<Assembly> m_assemblies;

        public struct Enumerator : IEnumerator<Type>
        {
            public Type Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly Assembly m_assembly;
            private IReadOnlyList<Assembly> m_assemblies;
            private int m_assemblyIndex;
            private Type[] m_types;
            private int m_typeIndex;
            private Type m_current;

            public Enumerator(Assembly assembly)
            {
                m_assembly = assembly;
                m_assemblies = null;
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            public Enumerator(IReadOnlyList<Assembly> assemblies)
            {
                m_assembly = null;
                m_assemblies = assemblies;
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (!Next())
                {
                    NextAssembly();
                }

                while (Next())
                {
                    Type type = m_types[m_typeIndex++];

                    if (type != null)
                    {
                        m_current = type;
                        return true;
                    }

                    if (!Next())
                    {
                        NextAssembly();
                    }
                }

                return false;
            }

            public void Reset()
            {
                m_assemblyIndex = 0;
                m_types = null;
                m_typeIndex = 0;
                m_current = null;
            }

            private bool Next()
            {
                return m_types != null && m_typeIndex < m_types.Length;
            }

            private void NextAssembly()
            {
                m_types = null;
                m_typeIndex = 0;

                if (m_assembly != null && m_assemblyIndex == 0)
                {
                    Type[] types = GetTypes(m_assembly);

                    m_assemblyIndex = 1;

                    if (types.Length > 0)
                    {
                        m_types = types;
                    }
                }
                else if (m_assemblies != null)
                {
                    while (m_assemblyIndex < m_assemblies.Count && m_types == null)
                    {
                        Type[] types = GetTypes(m_assemblies[m_assemblyIndex++]);

                        if (types.Length > 0)
                        {
                            m_types = types;
                        }
                    }
                }
            }

            private static Type[] GetTypes(Assembly assembly)
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException exception)
                {
                    return exception.Types;
                }
            }
        }

        /// <summary>
        /// Creates enumerable from the specified collection of the assembly.
        /// </summary>
        /// <param name="assembly">The assembly to enumerate.</param>
        public TypesAllEnumerable(Assembly assembly)
        {
            m_assembly = assembly;
            m_assemblies = null;
        }

        /// <summary>
        /// Creates enumerable from the specified collection of the assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to enumerate.</param>
        public TypesAllEnumerable(IReadOnlyList<Assembly> assemblies)
        {
            m_assembly = null;
            m_assemblies = assemblies;
        }

        public Enumerator GetEnumerator()
        {
            if (m_assembly != null)
            {
                return new Enumerator(m_assembly);
            }

            if (m_assemblies != null)
            {
                return new Enumerator(m_assemblies);
            }

            return new Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<Type> IEnumerable<Type>.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

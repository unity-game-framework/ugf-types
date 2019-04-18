using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UGF.Types.Runtime
{
    public struct TypesAllEnumerable : IEnumerable<Type>
    {
        private readonly IReadOnlyList<Assembly> m_assemblies;

        public struct Enumerator : IEnumerator<Type>
        {
            public Type Current { get { return m_current; } }

            object IEnumerator.Current { get { return m_current; } }

            private readonly IReadOnlyList<Assembly> m_assemblies;
            private int m_assemblyIndex;
            private Type[] m_types;
            private int m_typeIndex;
            private Type m_current;

            public Enumerator(IReadOnlyList<Assembly> assemblies)
            {
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

                while (m_assemblyIndex < m_assemblies.Count && m_types == null)
                {
                    Type[] types = GetTypes(m_assemblies[m_assemblyIndex++]);

                    if (types.Length > 0)
                    {
                        m_types = types;
                    }
                }
            }

            private Type[] GetTypes(Assembly assembly)
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

        public TypesAllEnumerable(IReadOnlyList<Assembly> assemblies)
        {
            m_assemblies = assemblies;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(m_assemblies);
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

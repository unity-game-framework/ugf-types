using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UnityEngine;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Provides utilities to work with types.
    /// </summary>
    public static class TypesUtility
    {
        /// <summary>
        /// Adds all found types marked with identifier attribute and register them into the specified provider.
        /// <para>
        /// Find types in all loaded assemblies.
        /// </para>
        /// </summary>
        /// <param name="provider">The type provider to register.</param>
        public static void GetTypes<T>(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            var types = new List<Type>();

            AssemblyUtility.GetBrowsableTypes(types, typeof(TypeIdentifierAttributeBase));

            AddTypes(provider, types);
        }

        /// <summary>
        /// Adds all found types marked with identifier attribute and register them into the specified provider from the specified assembly.
        /// <para>
        /// Find types only in the specified assembly.
        /// </para>
        /// </summary>
        /// <param name="provider">The type provider to register.</param>
        /// <param name="assembly">The assembly to search.</param>
        public static void GetTypes<T>(ITypeProvider<T> provider, Assembly assembly)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var types = new List<Type>();

            AssemblyUtility.GetBrowsableTypes(types, assembly, typeof(TypeIdentifierAttributeBase));

            AddTypes(provider, types);
        }

        /// <summary>
        /// Adds types from the specified collection to the specified type provider.
        /// </summary>
        /// <param name="provider">The type provider to register.</param>
        /// <param name="types">The collection of the types to add.</param>
        public static void AddTypes<T>(ITypeProvider<T> provider, IReadOnlyList<Type> types)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (types == null) throw new ArgumentNullException(nameof(types));

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (TryGetIdentifierFromType(type, out T identifier))
                {
                    provider.Add(identifier, type);
                }
            }
        }

        /// <summary>
        /// Collects all types into the specified collection that match by specified func condition, if presents.
        /// <para>
        /// If validate func does not specified, will add all types.
        /// </para>
        /// </summary>
        /// <param name="types">The collection to add types.</param>
        /// <param name="validate">The function to validate type.</param>
        public static void CollectTypes(ICollection<Type> types, Func<Type, bool> validate = null)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                CollectTypes(types, assemblies[i], validate);
            }
        }

        /// <summary>
        /// Collects all types from the specified assembly into the collection, that match by specified func condition, if presents.
        /// <para>
        /// If validate func does not specified, will add all types from the assembly.
        /// </para>
        /// </summary>
        /// <param name="types">The collection to add types.</param>
        /// <param name="assembly">The assembly to gather types.</param>
        /// <param name="validate">The function to validate type.</param>
        public static void CollectTypes(ICollection<Type> types, Assembly assembly, Func<Type, bool> validate = null)
        {
            if (types == null) throw new ArgumentNullException(nameof(types));
            if (assembly == null) throw new ArgumentNullException(nameof(types));

            Type[] assemblyTypes = assembly.GetTypes();

            for (int i = 0; i < assemblyTypes.Length; i++)
            {
                Type type = assemblyTypes[i];

                if (validate == null || validate(type))
                {
                    types.Add(type);
                }
            }
        }

        /// <summary>
        /// Tries to get type identifier from the specified type that contains identifier attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifier">The found identifier.</param>
        public static bool TryGetIdentifierFromType<T>(Type type, out T identifier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (type.GetCustomAttribute<TypeIdentifierAttributeBase>() is ITypeIdentifierAttribute<T> attribute)
            {
                identifier = attribute.Identifier;
                return true;
            }

            identifier = default;
            return false;
        }

        /// <summary>
        /// Tries to get type identifier from the specified type that contains identifier attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifier">The found identifier.</param>
        public static bool TryGetIdentifierFromType(Type type, out object identifier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var attribute = type.GetCustomAttribute<TypeIdentifierAttributeBase>();

            if (attribute != null)
            {
                identifier = attribute.GetIdentifier();
                return true;
            }

            identifier = null;
            return false;
        }

        public static void GetTypeDefines(ICollection<ITypeDefine> defines)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            var types = new List<Type>();

            AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(types);

            CreateTypes(defines, types, TypeCreationHandling.Warning);
        }

        public static void CreateTypes<T>(ICollection<T> results, IReadOnlyList<Type> types, TypeCreationHandling handling = TypeCreationHandling.Silent)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (types == null) throw new ArgumentNullException(nameof(types));

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (TryCreateType(type, handling, out T result))
                {
                    results.Add(result);
                }
            }
        }

        public static bool TryCreateType<T>(Type type, out T result)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return TryCreateType(type, TypeCreationHandling.Silent, out result);
        }

        public static bool TryCreateType<T>(Type type, TypeCreationHandling handling, out T result)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            try
            {
                result = (T)Activator.CreateInstance(type);
            }
            catch (Exception exception)
            {
                switch (handling)
                {
                    case TypeCreationHandling.Silent:
                    {
                        break;
                    }
                    case TypeCreationHandling.Warning:
                    {
                        Debug.LogWarning(exception);
                        break;
                    }
                    case TypeCreationHandling.Throw:
                    {
                        throw;
                    }
                    default: throw new ArgumentOutOfRangeException(nameof(handling), handling, null);
                }

                result = default;
                return false;
            }

            return true;
        }
    }
}

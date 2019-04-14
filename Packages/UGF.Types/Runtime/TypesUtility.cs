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
        public static void AddTypes<T>(ITypeProvider<T> provider, List<Type> types)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (types == null) throw new ArgumentNullException(nameof(types));

            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (type.GetCustomAttribute<TypeIdentifierAttributeBase>() is ITypeIdentifierAttribute<T> attribute)
                {
                    provider.Add(attribute.Identifier, type);
                }
                else
                {
                    Debug.LogWarning($"Can not add type to provider, cause '{typeof(ITypeIdentifierAttribute<T>)}' not found at specified type: '{type}'.");
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
        /// Collects all types the specified assembly into the specified collection, that match by specified func condition, if presents.
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
    }
}

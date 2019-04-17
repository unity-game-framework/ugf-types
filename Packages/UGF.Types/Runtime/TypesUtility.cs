using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Provides utilities to work with types.
    /// </summary>
    public static class TypesUtility
    {
        /// <summary>
        /// Gets type defines into specified collection.
        /// <para>
        /// If an assembly not specified, will search through the all assemblies.
        /// </para>
        /// </summary>
        /// <param name="defines">The collection to add found defines.</param>
        /// <param name="assembly">The assembly to search.</param>
        public static void GetTypeDefines(ICollection<ITypeDefine> defines, Assembly assembly = null)
        {
            if (defines == null) throw new ArgumentNullException(nameof(defines));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(assembly))
            {
                if (TryCreateType(type, out ITypeDefine define))
                {
                    defines.Add(define);
                }
            }
        }

        /// <summary>
        /// Adds all found types marked with identifier attribute and register them into the specified provider.
        /// <para>
        /// If an assembly not specified, will search through the all assemblies.
        /// </para>
        /// </summary>
        /// <param name="provider">The type provider to register.</param>
        /// <param name="assembly">The assembly to search.</param>
        /// <param name="includeDefines">Determines whether to include types from found type defines.</param>
        public static void GetTypes<T>(ITypeProvider<T> provider, Assembly assembly = null, bool includeDefines = true)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeIdentifierAttributeBase>(assembly))
            {
                if (TryGetIdentifierFromType(type, out T identifier))
                {
                    provider.Add(identifier, type);
                }
            }

            if (includeDefines)
            {
                foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(assembly))
                {
                    if (TryCreateType(type, out ITypeDefine<T> define))
                    {
                        define.Register(provider);
                    }
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

        public static bool TryCreateType<T>(Type type, out T result)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return TryCreateType(type, out result, out _);
        }

        public static bool TryCreateType<T>(Type type, out T result, out Exception exception)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            try
            {
                result = (T)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                result = default;
                exception = e;
                return false;
            }

            exception = null;
            return true;
        }
    }
}

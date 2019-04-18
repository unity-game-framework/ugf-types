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
        public static TypesAllEnumerable GetTypesAll()
        {
            return new TypesAllEnumerable(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// Gets type that contains type identifier attribute and supports specified identifier type and add them into specified collection.
        /// <para>
        /// If identifier type not specified, will collect all found defines.
        /// </para>
        /// <para>
        /// If an assembly not specified, will search through the all assemblies.
        /// </para>
        /// </summary>
        /// <param name="results">The collection to add found types.</param>
        /// <param name="identifierType">The identifier type of the type defines.</param>
        /// <param name="assembly">The assembly to search.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetTypes(ICollection<Type> results, Type identifierType = null, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeIdentifierAttribute>(assembly, inherit))
            {
                var attribute = type.GetCustomAttribute<TypeIdentifierAttribute>();

                if (identifierType == null || attribute.IdentifierType == identifierType)
                {
                    results.Add(type);
                }
            }
        }

        /// <summary>
        /// Gets defines that contains type define attribute and supports specified identifier type, and add them into specified collection.
        /// <para>
        /// If identifier type not specified, will collect all found defines.
        /// </para>
        /// <para>
        /// If an assembly not specified, will search through the all assemblies.
        /// </para>
        /// </summary>
        /// <param name="results">The collection to add found defines.</param>
        /// <param name="identifierType">The identifier type of the type defines.</param>
        /// <param name="assembly">The assembly to search.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetTypeDefines(ICollection<ITypeDefine> results, Type identifierType = null, Assembly assembly = null, bool inherit = true)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(assembly, inherit))
            {
                var attribute = type.GetCustomAttribute<TypeDefineAttribute>();

                if ((identifierType == null || attribute.IdentifierType == identifierType) && TryCreateType(type, out ITypeDefine define))
                {
                    results.Add(define);
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
        /// <param name="identifierType">The type of the identifier that provider supports.</param>
        /// <param name="assembly">The assembly to search.</param>
        /// <param name="includeDefines">Determines whether to include types from found type defines.</param>
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetTypes(ITypeProvider provider, Type identifierType, Assembly assembly = null, bool includeDefines = true, bool inherit = true)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (identifierType == null) throw new ArgumentNullException(nameof(identifierType));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeIdentifierAttribute>(assembly, inherit))
            {
                var attribute = type.GetCustomAttribute<TypeIdentifierAttribute>();

                if (attribute.IdentifierType == identifierType)
                {
                    provider.TryAdd(type);
                }
            }

            if (includeDefines)
            {
                foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(assembly, inherit))
                {
                    var attribute = type.GetCustomAttribute<TypeDefineAttribute>();

                    if (attribute.IdentifierType == identifierType && TryCreateType(type, out ITypeDefine define))
                    {
                        define.Register(provider);
                    }
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
        /// <param name="inherit">Determines whether to search in inheritance chain to find the attribute.</param>
        public static void GetTypes<T>(ITypeProvider<T> provider, Assembly assembly = null, bool includeDefines = true, bool inherit = true)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeIdentifierAttribute>(assembly, inherit))
            {
                if (TryGetIdentifierFromType(type, out T identifier))
                {
                    provider.Add(identifier, type);
                }
            }

            if (includeDefines)
            {
                foreach (Type type in AssemblyUtility.GetBrowsableTypes<TypeDefineAttribute>(assembly, inherit))
                {
                    var attribute = type.GetCustomAttribute<TypeDefineAttribute>();

                    if (attribute.IdentifierType == typeof(T) && TryCreateType(type, out ITypeDefine<T> define))
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

            if (type.GetCustomAttribute<TypeIdentifierAttribute>() is ITypeIdentifierAttribute<T> attribute)
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

            var attribute = type.GetCustomAttribute<TypeIdentifierAttribute>();

            if (attribute != null)
            {
                identifier = attribute.GetIdentifier();
                return true;
            }

            identifier = null;
            return false;
        }

        /// <summary>
        /// Tries to create specified type.
        /// <para>
        /// The type must contains default constructor.
        /// </para>
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <param name="result">The created result.</param>
        public static bool TryCreateType<T>(Type type, out T result)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return TryCreateType(type, out result, out _);
        }

        /// <summary>
        /// Tries to create specified type.
        /// <para>
        /// The type must contains default constructor.
        /// </para>
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <param name="result">The created result.</param>
        /// <param name="exception">The exception that could occurs during creation.</param>
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

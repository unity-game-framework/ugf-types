using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Types.Runtime.Attributes;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Provides utilities to work with types.
    /// </summary>
    public static class TypesUtility
    {
        /// <summary>
        /// Gets enumerable through the all loaded types.
        /// </summary>
        /// <param name="validation">The type validation.</param>
        public static TypesAllEnumerable GetTypesAll(ITypeValidation validation)
        {
            return GetTypesAll(null, validation);
        }

        /// <summary>
        /// Gets enumerable through the all loaded types.
        /// </summary>
        /// <param name="assembly">The assembly to search, if null, will search through the all assemblies.</param>
        /// <param name="validation">The type validation.</param>
        public static TypesAllEnumerable GetTypesAll(Assembly assembly = null, ITypeValidation validation = null)
        {
            return assembly != null
                ? new TypesAllEnumerable(assembly, validation)
                : new TypesAllEnumerable(AppDomain.CurrentDomain.GetAssemblies(), validation);
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
        public static void GetTypes(ICollection<Type> results, Type identifierType = null, Assembly assembly = null)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));

            foreach (Type type in GetTypesAll(assembly))
            {
                if (identifierType == null || TypesIdentifierUtility.TryGetIdentifierAttribute(type, identifierType, out _))
                {
                    results.Add(type);
                }
            }
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

        /// <summary>
        /// Tries to create specified type.
        /// <para>
        /// The type must contains default constructor.
        /// </para>
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <param name="result">The created result.</param>
        public static bool TryCreateType<T>(Type type, object[] arguments, out T result)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            return TryCreateType(type, arguments, out result, out _);
        }

        /// <summary>
        /// Tries to create specified type.
        /// <para>
        /// The type must contains default constructor.
        /// </para>
        /// </summary>
        /// <param name="type">The type to create.</param>
        /// <param name="arguments">The constructor arguments.</param>
        /// <param name="result">The created result.</param>
        /// <param name="exception">The exception that could occurs during creation.</param>
        public static bool TryCreateType<T>(Type type, object[] arguments, out T result, out Exception exception)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));

            try
            {
                result = (T)Activator.CreateInstance(type, arguments);
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

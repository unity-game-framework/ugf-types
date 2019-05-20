using System;
using System.Reflection;

namespace UGF.Types.Runtime
{
    public static partial class TypesUtility
    {
        /// <summary>
        /// Tries to get type identifier from the specified type that contains identifier attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifier">The found identifier.</param>
        [Obsolete("TryGetIdentifierFromType has been deprecated. Use overload with identifier type instead.")]
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
    }
}
using System;

namespace UGF.Types.Runtime.Attributes
{
    /// <summary>
    /// Provides utilities to work with identifier attributes.
    /// </summary>
    public static class TypesIdentifierUtility
    {
        /// <summary>
        /// Tries to get type identifier from the specified type that contains identifier attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifier">The found identifier.</param>
        public static bool TryGetIdentifierFromType<T>(Type type, out T identifier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (TryGetIdentifierAttribute(type, typeof(T), out TypeIdentifierAttribute attribute) && attribute is ITypeIdentifierAttribute<T> cast)
            {
                identifier = cast.Identifier;
                return true;
            }

            identifier = default;
            return false;
        }

        /// <summary>
        /// Tries to get type identifier from the specified type that contains identifier attribute.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifierType">The type of the identifier.</param>
        /// <param name="identifier">The found identifier.</param>
        public static bool TryGetIdentifierFromType(Type type, Type identifierType, out object identifier)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (identifierType == null) throw new ArgumentNullException(nameof(identifierType));

            if (TryGetIdentifierAttribute(type, identifierType, out TypeIdentifierAttribute attribute))
            {
                identifier = attribute.Identifier;
                return true;
            }

            identifier = null;
            return false;
        }

        /// <summary>
        /// Tries to get identifier type attribute from the specified type that support specified identifier type.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <param name="identifierType">The type of the identifier.</param>
        /// <param name="attribute">The found attribute.</param>
        public static bool TryGetIdentifierAttribute(Type type, Type identifierType, out TypeIdentifierAttribute attribute)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (identifierType == null) throw new ArgumentNullException(nameof(identifierType));

            object[] attributes = type.GetCustomAttributes(false);

            for (int i = 0; i < attributes.Length; i++)
            {
                if (attributes[i] is TypeIdentifierAttribute cast && cast.IdentifierType == identifierType)
                {
                    attribute = cast;
                    return true;
                }
            }

            attribute = null;
            return false;
        }
    }
}

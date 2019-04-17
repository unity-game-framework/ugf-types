using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    /// <summary>
    /// Represents attribute used to mark type definition classes.
    /// <para>
    /// The identifier type used to determines that this definition contains types that stored by the this specific identifier type.
    /// </para>
    /// </summary>
    [BaseTypeRequired(typeof(ITypeDefine))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TypeDefineAttribute : AssemblyBrowsableTypeAttribute
    {
        /// <summary>
        /// Gets the type of the identifier.
        /// </summary>
        public Type IdentifierType { get; }

        /// <summary>
        /// Creates attribute with the specified identifier type.
        /// </summary>
        /// <param name="identifierType">The type of the identifier.</param>
        public TypeDefineAttribute(Type identifierType)
        {
            IdentifierType = identifierType;
        }
    }
}

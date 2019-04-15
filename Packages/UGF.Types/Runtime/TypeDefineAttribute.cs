using System;
using JetBrains.Annotations;
using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    [BaseTypeRequired(typeof(ITypeDefine))]
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TypeDefineAttribute : AssemblyBrowsableTypeAttribute
    {
    }
}

using UGF.Assemblies.Runtime;

namespace UGF.Types.Runtime
{
    public abstract class TypeIdentifierAttributeBase : AssemblyBrowsableTypeAttribute
    {
        public abstract object GetIdentifier();
    }
}
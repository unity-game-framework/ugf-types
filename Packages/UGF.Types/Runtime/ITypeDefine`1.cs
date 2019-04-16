using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public interface ITypeDefine<TIdentifier>
    {
        Type IdentifierType { get; }
        IReadOnlyDictionary<TIdentifier, Type> Types { get; }

        void Register(ITypeProvider<TIdentifier> provider);
        void Unregister(ITypeProvider<TIdentifier> provider);
    }
}

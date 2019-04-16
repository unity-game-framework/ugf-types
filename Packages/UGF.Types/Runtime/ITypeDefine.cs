using System;
using System.Collections.Generic;

namespace UGF.Types.Runtime
{
    public interface ITypeDefine
    {
        Type IdentifierType { get; }
        IEnumerable<KeyValuePair<object, Type>> Types { get; }

        void Register(ITypeProvider provider);
        void Unregister(ITypeProvider provider);
    }
}

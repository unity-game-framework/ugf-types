using System;
using System.Collections;

namespace UGF.Types.Runtime
{
    public interface ITypeProvider : IEnumerable
    {
        int Count { get; }
        Type IdentifierType { get; }
    }
}
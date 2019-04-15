using System;

namespace UGF.Types.Runtime.Tests
{
    [TypeDefine]
    public class TestTypeDefine : TypeDefine<Guid>
    {
        public TestTypeDefine()
        {
            Types.Add(new Guid("f6246b5cbd6b4d489ab5411627b1c9d0"), typeof(bool));
            Types.Add(new Guid("c7e25152444e47e0a337f31757131040"), typeof(int));
            Types.Add(new Guid("b4655739603b412886199eb7fa8468fe"), typeof(float));
        }
    }
}

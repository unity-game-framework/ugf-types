using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace UGF.Types.Runtime.Tests
{
    public class TestTypesUtility
    {
        [TypeIdentifierGuid("f6ca48946268479d95729efbc8be5eda")]
        private class Target
        {
        }

        [Test]
        public void GetTypes()
        {
            var types = new List<Type>();

            TypesUtility.GetTypes(types, typeof(Guid));

            Assert.AreEqual(2, types.Count);
        }

        [Test]
        public void GetTypeDefines()
        {
            var defines = new List<ITypeDefine>();

            TypesUtility.GetTypeDefines(defines);

            Assert.AreEqual(1, defines.Count);

            ITypeDefine define = defines[0];

            Assert.NotNull(define);
            Assert.IsAssignableFrom<TestTypeDefine>(define);
        }

        [Test]
        public void GetTypesProvider()
        {
            ITypeProvider provider = new TypeProvider<Guid>();

            TypesUtility.GetTypes(provider, typeof(Guid), null, false);

            Assert.AreEqual(2, provider.Types.Count());
        }

        [Test]
        public void GetTypesProviderGeneric()
        {
            var provider = new TypeProvider<Guid>();

            TypesUtility.GetTypes(provider, null, false);

            Assert.AreEqual(2, provider.Types.Count);
        }

        [Test]
        public void GetTypesWithDefines()
        {
            var provider = new TypeProvider<Guid>();

            TypesUtility.GetTypes(provider);

            Assert.AreEqual(5, provider.Types.Count);
        }

        [Test]
        public void GetTypesFromAssembly()
        {
            var provider = new TypeProvider<Guid>();
            Assembly assembly = typeof(Target).Assembly;

            TypesUtility.GetTypes(provider, assembly, false);

            Assert.AreEqual(1, provider.Types.Count);
        }

        [Test]
        public void TryGetIdentifierFromTypeGeneric()
        {
            bool result = TypesUtility.TryGetIdentifierFromType(typeof(Target), out Guid id);

            Assert.True(result);
            Assert.AreEqual(new Guid("f6ca48946268479d95729efbc8be5eda"), id);
        }

        [Test]
        public void TryGetIdentifierFromType()
        {
            bool result = TypesUtility.TryGetIdentifierFromType(typeof(Target), out object id);

            Assert.True(result);
            Assert.AreEqual(new Guid("f6ca48946268479d95729efbc8be5eda"), id);
        }

        [Test]
        public void TryCreateType()
        {
            bool result = TypesUtility.TryCreateType(typeof(Target), out Target target);

            Assert.True(result);
            Assert.NotNull(target);
        }
    }
}

using System;
using System.Collections.Generic;
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
            var provider = new TypeProvider<Guid>();
            
            TypesUtility.GetTypes(provider);
            
            Assert.AreEqual(2, provider.Count);
        }

        [Test]
        public void GetTypesFromAssembly()
        {
            var provider = new TypeProvider<Guid>();
            Assembly assembly = typeof(Target).Assembly;
            
            TypesUtility.GetTypes(provider, assembly);
            
            Assert.AreEqual(1, provider.Count);
        }

        [Test]
        public void AddTypes()
        {
            var provider = new TypeProvider<Guid>();
            var types = new List<Type> { typeof(Target) };

            TypesUtility.AddTypes(provider, types);
            
            Assert.AreEqual(1, provider.Count);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using UGF.Types.Runtime.Attributes;

namespace UGF.Types.Runtime.Tests
{
    public class TestTypesUtility
    {
        [TypeIdentifierGuid("f6ca48946268479d95729efbc8be5eda")]
        private class Target
        {
        }

        private class Target2
        {
            public object Value { get; }

            public Target2(object value)
            {
                Value = value;
            }
        }

        [TypeIdentifierInt32(10)]
        [TypeIdentifierString("target")]
        private class Target3
        {
        }

        [Test]
        public void GetTypesAll()
        {
            int count0 = TypesUtility.GetTypesAll().Count();
            int count1 = AllTypes().Count();

            Assert.AreEqual(count1, count0);

            IEnumerable<Type> AllTypes()
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Type[] types;

                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException exception)
                    {
                        types = exception.Types;
                    }

                    foreach (Type type in types)
                    {
                        if (type != null)
                        {
                            yield return type;
                        }
                    }
                }
            }
        }

        [Test]
        public void GetTypes()
        {
            var types = new List<Type>();

            TypesUtility.GetTypes(types, typeof(Guid));

            Assert.AreEqual(2, types.Count);
        }

        [Test]
        public void GetTypesString()
        {
            var types = new List<Type>();

            TypesUtility.GetTypes(types, typeof(string));

            Assert.AreEqual(1, types.Count);
            Assert.Contains(typeof(Target3), types);
        }

        [Test]
        public void GetTypesInt32()
        {
            var types = new List<Type>();

            TypesUtility.GetTypes(types, typeof(int));

            Assert.AreEqual(1, types.Count);
            Assert.Contains(typeof(Target3), types);
        }

        [Test]
        public void TryGetIdentifierFromTypeGeneric()
        {
            bool result = TypesIdentifierUtility.TryGetIdentifierFromType(typeof(Target), out Guid id);

            Assert.True(result);
            Assert.AreEqual(new Guid("f6ca48946268479d95729efbc8be5eda"), id);
        }

        [Test]
        public void TryGetIdentifierFromType()
        {
            bool result = TypesIdentifierUtility.TryGetIdentifierFromType(typeof(Target), typeof(Guid), out object id);

            Assert.True(result);
            Assert.AreEqual(new Guid("f6ca48946268479d95729efbc8be5eda"), id);
        }

        [Test]
        public void TryGetIdentifierFromTypeString()
        {
            bool result = TypesIdentifierUtility.TryGetIdentifierFromType(typeof(Target3), out string id);

            Assert.True(result);
            Assert.AreEqual(id, "target");
        }

        [Test]
        public void TryGetIdentifierFromTypeInt32()
        {
            bool result = TypesIdentifierUtility.TryGetIdentifierFromType(typeof(Target3), out int id);

            Assert.True(result);
            Assert.AreEqual(10, id);
        }

        [Test]
        public void TryCreateType()
        {
            bool result = TypesUtility.TryCreateType(typeof(Target), out Target target);

            Assert.True(result);
            Assert.NotNull(target);
        }

        [Test]
        public void TryCreateTypeWithArguments()
        {
            bool result = TypesUtility.TryCreateType(typeof(Target2), new object[] { 10F }, out Target2 target);

            Assert.True(result);
            Assert.NotNull(target);
            Assert.AreEqual(10F, target.Value);
        }
    }
}

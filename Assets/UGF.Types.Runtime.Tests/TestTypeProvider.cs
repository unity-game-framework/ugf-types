using System;
using NUnit.Framework;

namespace UGF.Types.Runtime.Tests
{
    public class TestTypeProvider
    {
        private class Target
        {
        }

        [Test]
        public void Add()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();

            provider.Add(guid, typeof(Target));

            Assert.AreEqual(1, provider.Types.Count);
            Assert.True(provider.Types.ContainsKey(guid));
            Assert.True(provider.TryGetIdentifier(typeof(Target), out _));
        }

        [Test]
        public void RemoveIdentifier()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();

            provider.Add(guid, typeof(Target));
            provider.Remove(guid);

            Assert.AreEqual(0, provider.Types.Count);
            Assert.False(provider.Types.ContainsKey(guid));
            Assert.False(provider.TryGetIdentifier(typeof(Target), out _));
        }

        [Test]
        public void Clear()
        {
            var provider = new TypeProvider<Guid>();

            provider.Add(Guid.NewGuid(), typeof(Target));
            provider.Add(Guid.NewGuid(), typeof(bool));
            provider.Add(Guid.NewGuid(), typeof(int));

            provider.Clear();

            Assert.AreEqual(0, provider.Types.Count);
        }

        [Test]
        public void GetIdentifier()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();

            provider.Add(guid, typeof(Target));

            Guid guid0 = provider.GetIdentifier(typeof(Target));

            Assert.AreEqual(guid, guid0);
        }

        [Test]
        public void TryGetIdentifier()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();

            provider.Add(guid, typeof(Target));

            bool result0 = provider.TryGetIdentifier(typeof(Target), out Guid guid0);
            bool result1 = provider.TryGetIdentifier(typeof(bool), out Guid guid1);

            Assert.True(result0);
            Assert.False(result1);
            Assert.AreEqual(guid, guid0);
        }
    }
}

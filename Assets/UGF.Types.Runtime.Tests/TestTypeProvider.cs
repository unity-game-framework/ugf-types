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
        public void ContainsIdentifier()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();
            
            provider.Add(guid, typeof(Target));

            bool result0 = provider.Contains(guid);
            bool result1 = provider.Contains(Guid.NewGuid());
            
            Assert.True(result0);
            Assert.False(result1);
        }

        [Test]
        public void ContainsType()
        {
            var provider = new TypeProvider<Guid>();
            
            provider.Add(Guid.NewGuid(), typeof(Target));

            bool result0 = provider.Contains(typeof(Target));
            bool result1 = provider.Contains(typeof(bool));
            
            Assert.True(result0);
            Assert.False(result1);
        }

        [Test]
        public void Add()
        {
            var provider = new TypeProvider<Guid>();
            
            provider.Add(Guid.NewGuid(), typeof(Target));
            
            Assert.AreEqual(1, provider.Count);
            Assert.True(provider.Contains(typeof(Target)));
        }

        [Test]
        public void RemoveIdentifier()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();
            
            provider.Add(guid, typeof(Target));
            provider.Remove(guid);
            
            Assert.AreEqual(0, provider.Count);
            Assert.False(provider.Contains(guid));
            Assert.False(provider.Contains(typeof(Target)));
        }

        [Test]
        public void RemoveType()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();
            
            provider.Add(guid, typeof(Target));
            provider.Remove(typeof(Target));
            
            Assert.AreEqual(0, provider.Count);
            Assert.False(provider.Contains(guid));
            Assert.False(provider.Contains(typeof(Target)));
        }

        [Test]
        public void Clear()
        {
            var provider = new TypeProvider<Guid>();
            
            provider.Add(Guid.NewGuid(), typeof(Target));
            provider.Add(Guid.NewGuid(), typeof(bool));
            provider.Add(Guid.NewGuid(), typeof(int));
            
            provider.Clear();
            
            Assert.AreEqual(0, provider.Count);
        }

        [Test]
        public void Get()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();
            
            provider.Add(guid, typeof(Target));

            Type type = provider.Get(guid);
            
            Assert.NotNull(type);
            Assert.AreEqual(typeof(Target), type);
        }

        [Test]
        public void TryGet()
        {
            var provider = new TypeProvider<Guid>();
            Guid guid = Guid.NewGuid();
            
            provider.Add(guid, typeof(Target));

            bool result0 = provider.TryGet(guid, out Type type0);
            bool result1 = provider.TryGet(Guid.NewGuid(), out Type type1);
            
            Assert.True(result0);
            Assert.False(result1);
            Assert.NotNull(type0);
            Assert.Null(type1);
            Assert.AreEqual(typeof(Target), type0);
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
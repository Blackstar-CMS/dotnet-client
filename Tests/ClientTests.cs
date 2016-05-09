using System;
using System.Linq;
using NUnit.Framework;
using Blackstar;

namespace Tests
{
    [TestFixture]
    public class ClientTests
    {
        Client _client = new Client(new Uri("http://demo.blackstarcms.net"));

        [Test]
        public void GetAll()
        {
            var chunks = _client.Get.All().Result;
            Assert.IsNotEmpty(chunks);
            Assert.IsTrue(chunks.All(c => c.Id > 0));            
            Assert.IsTrue(chunks.All(c => c.Value == ""));            
        }

        [Test]
        public void GetByIds()
        {
            var chunks = _client.Get.ByIds(new[] { 28,30,32 }).Result;
            Assert.AreEqual(3, chunks.Count());
        }

        [Test]
        public void GetByNames()
        {
            var chunks = _client.Get.ByNames(new[] { "index-heading", "main-heading" }).Result;
            Assert.AreEqual(2, chunks.Count());
        }

        [Test]
        public void GetByTags()
        {
            var chunks = _client.Get.ByTags(new[] { "blackstarpedia" }).Result;
            Assert.Greater(chunks.Count(), 2);
        }

        [Test]
        public void NonExistingById()
        {
            var chunks = _client.Get.ByIds(new[] { 98374691 }).Result;
            Assert.IsEmpty(chunks);
        }
    }
}

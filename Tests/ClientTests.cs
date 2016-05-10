using System.Linq;
using NUnit.Framework;
using Blackstar;

namespace Tests
{
    [TestFixture]
    public class ClientTests
    {
        readonly BlackstarClient _client = new BlackstarClient("http://demo.blackstarcms.net");

        [Test]
        public void GetAll()
        {
            var chunks = _client.GetAllAsync().Result;
            Assert.IsNotEmpty(chunks);
            Assert.IsTrue(chunks.All(c => c.Id > 0));            
            Assert.IsTrue(chunks.All(c => c.Value == ""));            
        }

        [Test]
        public void GetByTags()
        {
            var chunks = _client.GetByTagsAsync(new[] { "blackstarpedia" }).Result;
            Assert.Greater(chunks.Length, 2);
        }

        [Test]
        public void GetByIds()
        {
            var chunks = _client.GetByIdsAsync(new[] { 28,30,32 }).Result;
            Assert.AreEqual(3, chunks.Length);
        }

        [Test]
        public void GetByNames()
        {
            var chunks = _client.GetByNamesAsync(new[] { "index-heading", "main-heading" }).Result;
            Assert.AreEqual(2, chunks.Length);
        }

        [Test]
        public void NonExistingById()
        {
            var chunks = _client.GetByIdsAsync(new[] { 98374691 }).Result;
            Assert.IsEmpty(chunks);
        }
    }
}

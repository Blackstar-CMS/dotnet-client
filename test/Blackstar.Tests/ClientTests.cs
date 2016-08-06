using System.Linq;
using Xunit;
using Blackstar;
using System;

namespace Tests
{
    public class ClientTests
    {
        readonly BlackstarClient _client = new BlackstarClient("http://demo.blackstarcms.net", "liam", "QaX4yGThxerOIXWSeaSM");

        [Fact]
        public void GetAll()
        {
            var chunks = _client.GetAllAsync().Result;
            Assert.NotEmpty(chunks);
            Assert.True(chunks.All(c => c.Id > 0));            
            Assert.True(chunks.All(c => c.Value == ""));            
        }

        [Fact]
        public void GetByTags()
        {
            var chunks = _client.GetByTagsAsync(new[] { "blackstarpedia" }).Result;
            Assert.True(chunks.Length > 2);
        }

        [Fact]
        public void GetByTag()
        {
            var chunks = _client.GetByTagAsync("blackstarpedia").Result;
            Assert.True(chunks.Length > 2);
        }

        [Fact]
        public void GetByIds()
        {
            var chunks = _client.GetByIdsAsync(new[] { 28,30,32 }).Result;
            Assert.Equal(3, chunks.Length);
        }

        [Fact]
        public void GetByNames()
        {
            var chunks = _client.GetByNamesAsync(new[] { "index-heading", "main-heading" }).Result;
            Assert.Equal(2, chunks.Length);
        }

        [Fact]
        public void NonExistingById()
        {
            var chunks = _client.GetByIdsAsync(new[] { 98374691 }).Result;
            Assert.Empty(chunks);
        }

        [Fact]
        public void WrongUrl()
        {
            Assert.Throws<AggregateException>(() => GetById()); 
        }

        private ContentChunk GetById() {
            var _client = new BlackstarClient("http://wrong.blackstarcms.net");
            return _client.GetByIdAsync(28).Result;
        }
    }
}

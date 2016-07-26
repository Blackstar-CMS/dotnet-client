using Blackstar;
using Xunit;
using System;

namespace Tests
{
    public class BlackstarServerRoutesTests
    {
        BlackstarServerRoutes routes = new BlackstarServerRoutes(new Uri("http://demo.blackstarcms.net"));

        [Fact]
        public void ApiUri()
        {
            AssertionExtensions.UriEqual("http://demo.blackstarcms.net/api/content", routes.AllChunks());
        }

        [Fact]
        public void GetByIds()
        {
            AssertionExtensions.UriEqual(
                "http://demo.blackstarcms.net/api/content/byids/1/2/3",
                routes.GetByIds(new[] { 1, 2, 3 }));
        }
    }
}

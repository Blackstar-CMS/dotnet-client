using Blackstar;
using NUnit.Framework;
using System;

namespace Tests
{
    [TestFixture]
    public class BlackstarServerRoutesTests
    {
        BlackstarServerRoutes routes;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            routes = new BlackstarServerRoutes(new Uri("http://demo.blackstarcms.net"));
        }

        [Test]
        public void ApiUri()
        {
            AssertionExtensions.UriEqual("http://demo.blackstarcms.net/api/content", routes.AllChunks());
        }

        [Test]
        public void GetByIds()
        {
            AssertionExtensions.UriEqual(
                "http://demo.blackstarcms.net/api/content/byids/1/2/3",
                routes.GetByIds(new[] { 1, 2, 3 }));
        }
    }
}

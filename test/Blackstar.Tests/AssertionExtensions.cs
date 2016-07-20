using System;
using Xunit;

namespace Tests
{
    public static class AssertionExtensions
    {
        public static void UriEqual(string expected, Uri actual)
        {
            Assert.Equal(expected, actual.ToString());
        }
    }
}

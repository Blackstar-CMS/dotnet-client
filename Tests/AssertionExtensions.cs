using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests
{
    public static class AssertionExtensions
    {
        public static void UriEqual(string expected, Uri actual)
        {
            Assert.AreEqual(expected, actual.ToString());
        }
    }
}

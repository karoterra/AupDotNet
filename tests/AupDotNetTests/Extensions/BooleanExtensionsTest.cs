using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.Extensions
{
    [TestClass]
    public class BooleanExtensionsTest
    {
        [DataTestMethod]
        [DataRow(new byte[] { 0, 0, 0, 0 }, false)]
        [DataRow(new byte[] { 1, 0, 0, 0 }, true)]
        [DataRow(new byte[] { 0, 1, 0, 0 }, true)]
        [DataRow(new byte[] { 0, 0, 1, 0 }, true)]
        [DataRow(new byte[] { 0, 0, 0, 1 }, true)]
        public void Test_ToBool(byte[] x, bool expected)
        {
            Assert.AreEqual(expected, x.ToBool());
            Assert.AreEqual(expected, new ReadOnlySpan<byte>(x).ToBool());
        }

        [DataTestMethod]
        [DataRow(false, new byte[] { 0, 0, 0, 0 })]
        [DataRow(true, new byte[] { 1, 0, 0, 0 })]
        public void Test_ToBytes(bool x, byte[] expected)
        {
            Assert.IsTrue(x.ToBytes().SequenceEqual(expected));
        }
    }
}

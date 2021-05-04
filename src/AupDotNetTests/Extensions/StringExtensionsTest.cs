using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [DataTestMethod]
        [DataRow("abcあいう", "abcあいう")]
        [DataRow("abcあいう", "abcあいう\0xxx")]
        [DataRow("", "\0abcあいう")]
        public void Test_CutNull(string dst, string src)
        {
            Assert.AreEqual(dst, src.CutNull());
        }

        [DataTestMethod]
        [DataRow("する", new byte[] { 0x82, 0xB7, 0x82, 0xE9 })]
        [DataRow("する", new byte[] { 0x82, 0xB7, 0x82, 0xE9, 0x00 })]
        [DataRow("する", new byte[] { 0x82, 0xB7, 0x82, 0xE9, 0x95 })]
        [DataRow("する", new byte[] { 0x82, 0xB7, 0x82, 0xE9, 0x95, 0x00 })]
        [DataRow("AあB", new byte[] { 0x41, 0x82, 0xA0, 0x42 })]
        [DataRow("AあB", new byte[] { 0x41, 0x82, 0xA0, 0x42, 0x00 })]
        [DataRow("AあB", new byte[] { 0x41, 0x82, 0xA0, 0x42, 0x82 })]
        [DataRow("AあB", new byte[] { 0x41, 0x82, 0xA0, 0x42, 0x82, 0x00 })]
        public void Test_ToCleanSjisString(string str, byte[] bytes)
        {
            Assert.AreEqual(str, bytes.ToCleanSjisString());
            Assert.AreEqual(str, new ReadOnlySpan<byte>(bytes).ToCleanSjisString());
        }
    }
}

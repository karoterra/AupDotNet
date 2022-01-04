using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.Extensions
{
    [TestClass]
    public class IntegerExtensionsTest
    {
        [TestMethod]
        public void Test_ToUInt16()
        {
            var bytes = new byte[] { 0x01, 0x00 };
            ushort ans = 1;
            Assert.AreEqual(bytes.ToUInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt16(), ans);

            bytes = new byte[] { 0xFF, 0xFF };
            ans = 0xFFFF;
            Assert.AreEqual(bytes.ToUInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt16(), ans);

            bytes = new byte[] { 0x34, 0x12 };
            ans = 0x1234;
            Assert.AreEqual(bytes.ToUInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt16(), ans);
        }

        [TestMethod]
        public void Test_ToInt16()
        {
            var bytes = new byte[] { 0x01, 0x00 };
            short ans = 1;
            Assert.AreEqual(bytes.ToInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt16(), ans);

            bytes = new byte[] { 0xFF, 0xFF };
            ans = -1;
            Assert.AreEqual(bytes.ToInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt16(), ans);

            bytes = new byte[] { 0x34, 0x12 };
            ans = 0x1234;
            Assert.AreEqual(bytes.ToInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt16(), ans);

            bytes = new byte[] { 0x2E, 0xFB };
            ans = -1234;
            Assert.AreEqual(bytes.ToInt16(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt16(), ans);
        }

        [TestMethod]
        public void Test_ToUInt32()
        {
            var bytes = new byte[] { 0x01, 0x00, 0x00, 0x00 };
            uint ans = 1;
            Assert.AreEqual(bytes.ToUInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt32(), ans);

            bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            ans = 0xFFFF_FFFF;
            Assert.AreEqual(bytes.ToUInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt32(), ans);

            bytes = new byte[] { 0x78, 0x56, 0x34, 0x12 };
            ans = 0x1234_5678;
            Assert.AreEqual(bytes.ToUInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToUInt32(), ans);
        }

        [TestMethod]
        public void Test_ToInt32()
        {
            var bytes = new byte[] { 0x01, 0x00, 0x00, 0x00 };
            int ans = 1;
            Assert.AreEqual(bytes.ToInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt32(), ans);

            bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF };
            ans = -1;
            Assert.AreEqual(bytes.ToInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt32(), ans);

            bytes = new byte[] { 0x78, 0x56, 0x34, 0x12 };
            ans = 0x1234_5678;
            Assert.AreEqual(bytes.ToInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt32(), ans);

            bytes = new byte[] { 0xEB, 0x32, 0xA4, 0xF8 };
            ans = -123456789;
            Assert.AreEqual(bytes.ToInt32(), ans);
            Assert.AreEqual(new ReadOnlySpan<byte>(bytes).ToInt32(), ans);
        }

        [TestMethod]
        public void Test_ToBytesInt32()
        {
            int x = 1_234_567_890;
            var y = new byte[] { 0xD2, 0x02, 0x96, 0x49 };
            Assert.IsTrue(x.ToBytes().SequenceEqual(y));

            x = -123_456_789;
            y = new byte[] { 0xEB, 0x32, 0xA4, 0xF8 };
            Assert.IsTrue(x.ToBytes().SequenceEqual(y));
        }

        [TestMethod]
        public void Test_ToBytesUInt32()
        {
            uint x = 1_234_567_890;
            var y = new byte[] { 0xD2, 0x02, 0x96, 0x49 };
            Assert.IsTrue(x.ToBytes().SequenceEqual(y));

            x = 2_309_737_967;
            y = new byte[] { 0xEF, 0xCD, 0xAB, 0x89 };
            Assert.IsTrue(x.ToBytes().SequenceEqual(y));
        }
    }
}

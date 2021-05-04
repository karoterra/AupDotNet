using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Karoterra.AupDotNet;

namespace AupDotNetTests
{
    [TestClass]
    public class AviUtlProjectTest
    {
        [DataTestMethod]
        [DataRow(new byte[] { 0, 0, 0, 0 }, new byte[] { 0x84, 0x00 })]
        [DataRow(new byte[] { 1, 1, 1, 1, 1 }, new byte[] { 0x85, 0x01 })]
        [DataRow(new byte[] { 2, 2, 2, 2, 3, 3, 3, 3 }, new byte[] { 0x84, 0x02, 0x84, 0x03 })]
        [DataRow(new byte[] { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3 }, new byte[] { 0x85, 0x02, 0x86, 0x03 })]
        [DataRow(new byte[] { 1, 2, 3, 4 }, new byte[] { 0x04, 1, 2, 3, 4 })]
        [DataRow(new byte[] { 1, 2, 3, 4, 5, 6 }, new byte[] { 0x06, 1, 2, 3, 4, 5, 6 })]
        [DataRow(new byte[] { 1, 2, 3, 4, 5, 6, 0, 0, 0, 0, 0, 0 }, new byte[] { 0x06, 1, 2, 3, 4, 5, 6, 0x86, 0 })]
        [DataRow(new byte[] { 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 5, 6 }, new byte[] { 0x86, 0, 0x06, 1, 2, 3, 4, 5, 6 })]
        public void Test_Comp(byte[] input, byte[] output)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                AviUtlProject.Comp(writer, input);
                stream.Position = 0;
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                Assert.IsTrue(output.SequenceEqual(bytes));
            }
        }
    }
}

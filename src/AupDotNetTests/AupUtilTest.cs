using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet;

namespace AupDotNetTests
{
    [TestClass]
    public class AupUtilTest
    {
        [DataTestMethod]
        [DataRow("empty")]
        [DataRow("size1")]
        [DataRow("size2")]
        [DataRow("size3")]
        [DataRow("size4_comp")]
        [DataRow("size4_raw")]
        [DataRow("size5_comp")]
        [DataRow("size5_raw")]
        [DataRow("size127_comp")]
        [DataRow("size127_raw")]
        [DataRow("size128_comp")]
        [DataRow("size128_raw")]
        [DataRow("c4c4")]
        [DataRow("c5c6")]
        [DataRow("c130c4")]
        [DataRow("r3c4")]
        [DataRow("c4r3")]
        public void Test_Comp(string name)
        {
            byte[] raw = File.ReadAllBytes($@"TestData\CompDecomp\{name}_raw.dat");
            byte[] comp = File.ReadAllBytes($@"TestData\CompDecomp\{name}_comp.dat");

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                AupUtil.Comp(writer, raw);
                stream.Position = 0;
                var actual = new byte[stream.Length];
                stream.Read(actual, 0, actual.Length);
                Assert.IsTrue(comp.SequenceEqual(actual), name);
            }
        }

        [DataTestMethod]
        [DataRow("empty")]
        [DataRow("size1")]
        [DataRow("size2")]
        [DataRow("size3")]
        [DataRow("size4_comp")]
        [DataRow("size4_raw")]
        [DataRow("size5_comp")]
        [DataRow("size5_raw")]
        [DataRow("size127_comp")]
        [DataRow("size127_raw")]
        [DataRow("size128_comp")]
        [DataRow("size128_raw")]
        [DataRow("c4c4")]
        [DataRow("c5c6")]
        [DataRow("c130c4")]
        [DataRow("r3c4")]
        [DataRow("c4r3")]
        public void Test_Decomp(string name)
        {
            byte[] raw = File.ReadAllBytes($@"TestData\CompDecomp\{name}_raw.dat");

            using (var stream = File.OpenRead($@"TestData\CompDecomp\{name}_comp.dat"))
            using (var reader = new BinaryReader(stream))
            {
                var actual = new byte[raw.Length];
                AupUtil.Decomp(reader, actual);
                Assert.IsTrue(raw.SequenceEqual(actual), name);
            }
        }
    }
}

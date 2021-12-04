using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Karoterra.AupDotNet;

namespace AupDotNetTests
{
    [TestClass]
    public class AviUtlProjectTest
    {
        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_DataBeforeFooter(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            Assert.AreEqual(0, aup.DataBeforeFooter.Length);
        }

        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        public void Test_ReadWriteReadWrite(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            using (var br = new BinaryReader(ms))
            {
                aup.Write(bw);
                ms.Position = 0;
                var expected = ms.ToArray();

                aup = new AviUtlProject(br);
                ms.Position = 0;

                aup.Write(bw);
                ms.Position = 0;
                var actual = ms.ToArray();
                Assert.IsTrue(expected.SequenceEqual(actual));
            }
        }
    }
}

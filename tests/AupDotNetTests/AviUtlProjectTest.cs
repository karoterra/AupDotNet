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
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        public void Test_DataBeforeFooter(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            Assert.AreEqual(0, aup.DataBeforeFooter.Length);
        }

        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
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

        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        [DataRow(@"TestData\FilterProject\VariousFilters.aup")]
        [DataRow(@"TestData\Exedit\EffectSet01.aup")]
        [DataRow(@"TestData\Exedit\LayerScene.aup")]
        [DataRow(@"TestData\Exedit\Trackbar.aup")]
        [DataRow(@"TestData\Exedit\Chain.aup")]
        [DataRow(@"TestData\Exedit\Group.aup")]
        public void Test_FilterProjects(string filename)
        {
            AviUtlProject aup1 = new AviUtlProject(filename);

            string dirname = Path.GetDirectoryName(filename);
            string stem = Path.GetFileNameWithoutExtension(filename);
            var dataPaths = Directory.GetFiles(dirname, $"{stem}_Filter_*.dat");
            Assert.AreEqual(dataPaths.Length, aup1.FilterProjects.Count);

            for (int i = 0; i < aup1.FilterProjects.Count; i++)
            {
                FilterProject filter = aup1.FilterProjects[i];
                string dataPath = Path.Combine(
                    dirname,
                    $"{stem}_Filter_{i}_{filter.Name}.dat");
                var data = File.ReadAllBytes(dataPath);
                CollectionAssert.AreEqual(data, filter.DumpData());
            }

            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            using (var br = new BinaryReader(ms))
            {
                aup1.Write(bw);
                ms.Position = 0;
                AviUtlProject aup2 = new AviUtlProject(br);
                Assert.AreEqual(aup1.FilterProjects.Count, aup2.FilterProjects.Count, "FilterProjects count");
                for (int i = 0; i < aup1.FilterProjects.Count; i++)
                {
                    FilterProject f1 = aup1.FilterProjects[i];
                    FilterProject f2 = aup2.FilterProjects[i];
                    Assert.AreEqual(f1.Name, f2.Name, $"Filter name {i}");
                    CollectionAssert.AreEqual(f1.DumpData(), f2.DumpData());
                }
            }
        }
    }
}

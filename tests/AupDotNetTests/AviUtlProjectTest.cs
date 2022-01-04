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
        public void Test_FrameData(string filename)
        {
            AviUtlProject aup1 = new AviUtlProject(filename);

            string csvPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_FrameData.csv");
            var csv = File.ReadAllLines(csvPath);
            Assert.AreEqual(csv.Length, aup1.Frames.Count);
            for (int i = 0; i < csv.Length; i++)
            {
                var elements = csv[i].Split(',');
                FrameData expected = new FrameData()
                {
                    Video = uint.Parse(elements[0]),
                    Audio = uint.Parse(elements[1]),
                    Field2 = uint.Parse(elements[2]),
                    Field3 = uint.Parse(elements[3]),
                    Inter = byte.Parse(elements[4]),
                    Index24Fps = byte.Parse(elements[5]),
                    EditFlag = byte.Parse(elements[6]),
                    Config = byte.Parse(elements[7]),
                    Vcm = byte.Parse(elements[8]),
                    Field9 = byte.Parse(elements[9]),
                };
                Assert.IsTrue(expected.Equals(aup1.Frames[i]));
            }

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            using var br = new BinaryReader(ms);

            aup1.Write(bw);
            ms.Position = 0;
            AviUtlProject aup2 = new AviUtlProject(br);

            Assert.AreEqual(aup1.Frames.Count, aup2.Frames.Count, "Frames count");
            for (int i = 0; i < aup1.Frames.Count; i++)
            {
                Assert.IsTrue(aup1.Frames[i].Equals(aup2.Frames[i]), $"Frames[{i}]");
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

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);
            using var br = new BinaryReader(ms);
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

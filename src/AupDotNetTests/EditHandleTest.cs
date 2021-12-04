using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Karoterra.AupDotNet;

namespace AupDotNetTests
{
    public class EditHandleData
    {
        public string EditFilename { get; set; }
        public string OutputFilename { get; set; }
        public string ProjectFilename { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FrameNum { get; set; }
        public int SelectedFrameStart { get; set; }
        public int SelectedFrameEnd { get; set; }
        public int CurrentFrame { get; set; }
        public short AudioCh { get; set; }
        public int AudioRate { get; set; }
        public int VideoScale { get; set; }
        public int VideoRate { get; set; }
    }

    [TestClass]
    public class EditHandleTest
    {
        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        public void Test_Read(string filename)
        {
            AviUtlProject aup = new AviUtlProject(filename);
            string jsonPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_edithandle.json");
            string jsonText = File.ReadAllText(jsonPath);
            EditHandleData expected = JsonSerializer.Deserialize<EditHandleData>(jsonText);

            Assert.AreEqual(expected.EditFilename, aup.EditHandle.EditFilename, "EditFilename");
            Assert.AreEqual(expected.OutputFilename, aup.EditHandle.OutputFilename, "OutputFilename");
            Assert.AreEqual(expected.ProjectFilename, aup.EditHandle.ProjectFilename, "ProjectFilename");
            Assert.AreEqual(expected.Width, aup.EditHandle.Width, "Width");
            Assert.AreEqual(expected.Height, aup.EditHandle.Height, "Height");
            Assert.AreEqual(expected.FrameNum, aup.EditHandle.FrameNum, "FrameNum");
            Assert.AreEqual(expected.SelectedFrameStart, aup.EditHandle.SelectedFrameStart, "SelectedFrameStart");
            Assert.AreEqual(expected.SelectedFrameEnd, aup.EditHandle.SelectedFrameEnd, "SelectedFrameEnd");
            Assert.AreEqual(expected.CurrentFrame, aup.EditHandle.CurrentFrame, "CurrentFrame");
            Assert.AreEqual(expected.AudioCh, aup.EditHandle.AudioCh, "AudioCh");
            Assert.AreEqual(expected.AudioRate, aup.EditHandle.AudioRate, "AudioRate");
            Assert.AreEqual(expected.VideoScale, aup.EditHandle.VideoScale, "VideoScale");
            Assert.AreEqual(expected.VideoRate, aup.EditHandle.VideoRate, "VideoRate");
        }

        [DataTestMethod]
        [DataRow(@"TestData\EditHandle\640x480_2997-100fps_44100Hz.aup")]
        public void Test_ReadWriteRead(string filename)
        {
            AviUtlProject srcAup = new AviUtlProject(filename);
            AviUtlProject dstAup;
            using (var ms = new MemoryStream())
            using (var writer = new BinaryWriter(ms))
            using (var reader = new BinaryReader(ms))
            {
                srcAup.Write(writer);
                ms.Position = 0;
                dstAup = new AviUtlProject(reader);
            }

            var src = srcAup.EditHandle;
            var dst = dstAup.EditHandle;

            Assert.AreEqual(src.Flag, dst.Flag, "Flag");
            Assert.AreEqual(src.EditFilename, dst.EditFilename, "EditFilename");
            Assert.AreEqual(src.OutputFilename, dst.OutputFilename, "OutputFilename");
            Assert.AreEqual(src.ProjectFilename, dst.ProjectFilename, "ProjectFilename");
            Assert.AreEqual(src.Width, dst.Width, "Width");
            Assert.AreEqual(src.Height, dst.Height, "Height");
            Assert.AreEqual(src.FrameNum, dst.FrameNum, "FrameNum");
            Assert.AreEqual(src.SelectedFrameStart, dst.SelectedFrameStart, "SelectedFrameStart");
            Assert.AreEqual(src.SelectedFrameEnd, dst.SelectedFrameEnd, "SelectedFrameEnd");
            Assert.AreEqual(src.CurrentFrame, dst.CurrentFrame, "CurrentFrame");
            Assert.AreEqual(src.VideoDecodeBit, dst.VideoDecodeBit, "VideoDecodeBit");
            Assert.AreEqual(src.VideoDecodeFormat, dst.VideoDecodeFormat, "VideoDecodeFormat");
            Assert.AreEqual(src.AudioCh, dst.AudioCh, "AudioCh");
            Assert.AreEqual(src.AudioRate, dst.AudioRate, "AudioRate");
            Assert.AreEqual(src.VideoScale, dst.VideoScale, "VideoScale");
            Assert.AreEqual(src.VideoRate, dst.VideoRate, "VideoRate");

            Assert.IsTrue(src.ConfigNames.SequenceEqual(dst.ConfigNames), "ConfigNames");
            Assert.IsTrue(src.ImageHandles.SequenceEqual(dst.ImageHandles), "ImageHandles");

            var srcData = new ReadOnlySpan<byte>(src.Data);
            var dstData = new ReadOnlySpan<byte>(dst.Data);
            int start = 0;
            int length = 0x20d18 - EditHandle.UncompressedSize;
            Assert.IsTrue(srcData.Slice(start, length).SequenceEqual(dstData.Slice(start, length)));
            start = 0x20d18 - EditHandle.UncompressedSize + EditHandle.MaxFilename * EditHandle.MaxConfigFiles;
            length = 0x4bbd98 - EditHandle.UncompressedSize - start;
            Assert.IsTrue(srcData.Slice(start, length).SequenceEqual(dstData.Slice(start, length)));
            start = 0x4bbd98 - EditHandle.UncompressedSize + 4 * EditHandle.MaxImages;
            Assert.IsTrue(srcData.Slice(start).SequenceEqual(dstData.Slice(start)));
        }
    }
}

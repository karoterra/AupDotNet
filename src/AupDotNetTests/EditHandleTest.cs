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
        public void Test_ReadWrite(string filename)
        {
            AviUtlProject src = new AviUtlProject(filename);
            string dstPath = Path.Combine(
                Path.GetDirectoryName(filename),
                Path.GetFileNameWithoutExtension(filename) + "_parsed.aup");
            using (var writer = new BinaryWriter(File.OpenWrite(dstPath)))
            {
                src.Write(writer);
            }
            AviUtlProject dst = new AviUtlProject(dstPath);

            Assert.AreEqual(src.EditHandle.Flag, dst.EditHandle.Flag, "Flag");
            Assert.AreEqual(src.EditHandle.EditFilename, dst.EditHandle.EditFilename, "EditFilename");
            Assert.AreEqual(src.EditHandle.OutputFilename, dst.EditHandle.OutputFilename, "OutputFilename");
            Assert.AreEqual(src.EditHandle.ProjectFilename, dst.EditHandle.ProjectFilename, "ProjectFilename");
            Assert.AreEqual(src.EditHandle.Width, dst.EditHandle.Width, "Width");
            Assert.AreEqual(src.EditHandle.Height, dst.EditHandle.Height, "Height");
            Assert.AreEqual(src.EditHandle.FrameNum, dst.EditHandle.FrameNum, "FrameNum");
            Assert.AreEqual(src.EditHandle.SelectedFrameStart, dst.EditHandle.SelectedFrameStart, "SelectedFrameStart");
            Assert.AreEqual(src.EditHandle.SelectedFrameEnd, dst.EditHandle.SelectedFrameEnd, "SelectedFrameEnd");
            Assert.AreEqual(src.EditHandle.CurrentFrame, dst.EditHandle.CurrentFrame, "CurrentFrame");
            Assert.AreEqual(src.EditHandle.VideoDecodeBit, dst.EditHandle.VideoDecodeBit, "VideoDecodeBit");
            Assert.AreEqual(src.EditHandle.VideoDecodeFormat, dst.EditHandle.VideoDecodeFormat, "VideoDecodeFormat");
            Assert.AreEqual(src.EditHandle.AudioCh, dst.EditHandle.AudioCh, "AudioCh");
            Assert.AreEqual(src.EditHandle.AudioRate, dst.EditHandle.AudioRate, "AudioRate");
            Assert.AreEqual(src.EditHandle.VideoScale, dst.EditHandle.VideoScale, "VideoScale");
            Assert.AreEqual(src.EditHandle.VideoRate, dst.EditHandle.VideoRate, "VideoRate");

            Assert.IsTrue(src.EditHandle.ConfigNames.SequenceEqual(src.EditHandle.ConfigNames), "ConfigNames");
            Assert.IsTrue(src.EditHandle.ImageHandles.SequenceEqual(src.EditHandle.ImageHandles), "ImageHandles");
        }
    }
}

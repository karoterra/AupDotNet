using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AudioFileEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 0);
            var audio = obj.Effects[0] as AudioFileEffect;
            Assert.IsNotNull(audio);
            Assert.AreEqual(false, audio.Loop, "Loop");
            Assert.AreEqual(true, audio.LinkVideo, "LinkVideo");
            Assert.AreEqual(@"D:\TestData\sample.avi", audio.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 1, 5);
            audio = obj.Effects[0] as AudioFileEffect;
            Assert.IsNotNull(audio);
            Assert.AreEqual(1, audio.Position.Current, "Position.Current");
            Assert.AreEqual(100, audio.Speed.Current, "Speed.Current");
            Assert.AreEqual(true, audio.Loop, "Loop");
            Assert.AreEqual(false, audio.LinkVideo, "LinkVideo");
            Assert.AreEqual(@"D:\TestData\sample.wav", audio.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var audio = new AudioFileEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = audio.DumpExtData();
            int len = audio.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = audio.MaxFilenameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 0);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 1, 5);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

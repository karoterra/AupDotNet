using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class VideoFileEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[0] as VideoFileEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Position.Current, "Position.Current");
            Assert.AreEqual(2, effect.Speed.Current, "Speed.Currnet");
            Assert.AreEqual(true, effect.Loop, "Loop");
            Assert.AreEqual(false, effect.Alpha, "Alpha");
            Assert.AreEqual(@"D:\TestData\sample.avi", effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            effect = obj.Effects[0] as VideoFileEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Loop, "Loop");
            Assert.AreEqual(true, effect.Alpha, "Alpha");
            Assert.AreEqual("", effect.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var video = new VideoFileEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = video.DumpExtData();
            int len = video.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = VideoFileEffect.MaxFilenameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

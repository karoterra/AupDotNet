using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class VideoCompositionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            var effect = obj.Effects[6] as VideoCompositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Position.Current, "Position.Current");
            Assert.AreEqual(20, effect.Speed.Current, "Speed.Current");
            Assert.AreEqual(3, effect.X.Current, "X.Current");
            Assert.AreEqual(4, effect.Y.Current, "Y.Current");
            Assert.AreEqual(50, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(true, effect.LoopPlayback, "LoopPlayback");
            Assert.AreEqual(false, effect.Sync, "Sync");
            Assert.AreEqual(false, effect.LoopImage, "LoopImage");
            Assert.AreEqual(@"D:\TestData\sample.avi", effect.Filename, "Filename");
            Assert.AreEqual(0, effect.Mode, "Mode");

            effect = obj.Effects[7] as VideoCompositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.LoopPlayback, "LoopPlayback");
            Assert.AreEqual(true, effect.Sync, "Sync");
            Assert.AreEqual(false, effect.LoopImage, "LoopImage");
            Assert.AreEqual(@"", effect.Filename, "Filename");
            Assert.AreEqual(1, effect.Mode, "Mode");

            effect = obj.Effects[8] as VideoCompositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.LoopPlayback, "LoopPlayback");
            Assert.AreEqual(false, effect.Sync, "Sync");
            Assert.AreEqual(true, effect.LoopImage, "LoopImage");
            Assert.AreEqual(@"", effect.Filename, "Filename");
            Assert.AreEqual(2, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var video = new VideoCompositionEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = video.DumpExtData();
            int len = video.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = VideoCompositionEffect.MaxFilenameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[6] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "3");
        }
    }
}

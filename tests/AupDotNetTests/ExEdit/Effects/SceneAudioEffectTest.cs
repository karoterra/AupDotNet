using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class SceneAudioEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 85);
            var effect = obj.Effects[0] as SceneAudioEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Position.Current, "Position.Current");
            Assert.AreEqual(200, effect.Speed.Current, "Speed.Current");
            Assert.AreEqual(false, effect.Loop, "Loop");
            Assert.AreEqual(true, effect.Link, "Link");
            Assert.AreEqual(1, effect.Scene, "Scene");

            obj = ExeditTestUtil.GetObject(exedit, 0, 1, 90);
            effect = obj.Effects[0] as SceneAudioEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Loop, "Loop");
            Assert.AreEqual(false, effect.Link, "Link");
            Assert.AreEqual(2, effect.Scene, "Scene");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var scene = new SceneAudioEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = scene.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 1, 85);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 1, 90);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MirrorEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            var effect = obj.Effects[9] as MirrorEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Transparency.Current, "Transparency.Current");
            Assert.AreEqual(2, effect.Decay.Current, "Decay.Current");
            Assert.AreEqual(3, effect.Border.Current, "Border.Current");
            Assert.AreEqual(false, effect.Centering, "Centering");
            Assert.AreEqual(1, effect.Direction, "Direction");

            effect = obj.Effects[10] as MirrorEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Centering, "Centering");
            Assert.AreEqual(3, effect.Direction, "Direction");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var mirror = new MirrorEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = mirror.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "2");
        }
    }
}

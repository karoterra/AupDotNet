using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class LuminanceKeyEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[3] as LuminanceKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(2048, effect.Reference.Current, "Reference.Current");
            Assert.AreEqual(512, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(0, effect.TransparentType, "TransparentType");

            effect = obj.Effects[4] as LuminanceKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(3, effect.TransparentType, "TransparentType");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var luminanceKey = new LuminanceKeyEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = luminanceKey.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "2");
        }
    }
}

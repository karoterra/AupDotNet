using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class WipeEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            var effect = obj.Effects[6] as WipeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(50, effect.In.Current, "In.Current");
            Assert.AreEqual(25, effect.Out.Current, "Out.Current");
            Assert.AreEqual(2, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(true, effect.ReverseIn, "ReverseIn");
            Assert.AreEqual(false, effect.ReverseOut, "ReverseOut");
            Assert.AreEqual(1, effect.WipeType, "WipeType");
            Assert.AreEqual("", effect.Filename, "Filename");

            effect = obj.Effects[7] as WipeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.ReverseIn, "ReverseIn");
            Assert.AreEqual(true, effect.ReverseOut, "ReverseOut");
            Assert.AreEqual(0, effect.WipeType, "WipeType");
            Assert.AreEqual("sampleTransition", effect.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var wipe = new WipeEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = wipe.DumpExtData();
            var len = 4 + wipe.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            CheckDumpExtData(obj.Effects[6] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "2");
        }
    }
}

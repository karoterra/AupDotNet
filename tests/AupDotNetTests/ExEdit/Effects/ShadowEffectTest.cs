using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ShadowEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[7] as ShadowEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(-40, effect.X.Current, "X.Current");
            Assert.AreEqual(24, effect.Y.Current, "Y.Current");
            Assert.AreEqual(400, effect.Depth.Current, "Depth.Current");
            Assert.AreEqual(10, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(false, effect.Split, "Split");
            Assert.AreEqual(Color.FromArgb(0, 0, 0, 0), effect.Color, "Color");
            Assert.AreEqual("", effect.Filename, "Filename");

            effect = obj.Effects[8] as ShadowEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Split, "Split");
            Assert.AreEqual(@"D:\TestData\sample.bmp", effect.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var shadow = new ShadowEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = shadow.DumpExtData();
            var len = 4 + shadow.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "2");
        }
    }
}

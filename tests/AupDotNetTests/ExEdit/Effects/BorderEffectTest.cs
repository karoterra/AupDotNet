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
    public class BorderEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[9] as BorderEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(3, effect.Size.Current, "Size.Current");
            Assert.AreEqual(10, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(@"D:\TestData\sample2.bmp", effect.Filename, "Filename");

            effect = obj.Effects[10] as BorderEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual("", effect.Filename, "Filename");
            Assert.AreEqual(Color.FromArgb(0, 201, 18, 172), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var border = new BorderEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = border.DumpExtData();
            var len = 4 + border.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "2");
        }
    }
}

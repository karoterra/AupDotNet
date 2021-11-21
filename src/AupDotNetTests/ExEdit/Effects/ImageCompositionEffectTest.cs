using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ImageCompositionEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            var effect = obj.Effects[9] as ImageCompositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(1000, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(false, effect.Loop, "Loop");
            Assert.AreEqual(@"D:\TestData\サンプル.bmp", effect.Filename, "Filename");
            Assert.AreEqual(3, effect.Mode, "Mode");

            effect = obj.Effects[10] as ImageCompositionEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Loop, "Loop");
            Assert.AreEqual(@"", effect.Filename, "Filename");
            Assert.AreEqual(4, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var image = new ImageCompositionEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = image.DumpExtData();
            int len = 4 + image.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "2");
        }
    }
}

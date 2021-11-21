using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorKeyEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[1] as ColorKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.LuminanceRange.Current, "LuminanceRange.Current");
            Assert.AreEqual(2, effect.DifferenceRange.Current, "DifferenceRange.Current");
            Assert.AreEqual(3, effect.Boundary.Current, "Boundary.Current");
            Assert.AreEqual(0, effect.Status, "Status");

            effect = obj.Effects[2] as ColorKeyEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Status, "Status");
            Assert.AreEqual(new YCbCr(1524, 989, -1087), effect.Color, "Color");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var chromaKey = new ColorKeyEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = chromaKey.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "2");
        }
    }
}

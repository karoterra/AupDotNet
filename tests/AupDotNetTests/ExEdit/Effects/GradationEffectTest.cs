using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class GradationEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[6] as GradationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(2, effect.X.Current, "X.Current");
            Assert.AreEqual(3, effect.Y.Current, "Y.Current");
            Assert.AreEqual(40, effect.Angle.Current, "Angle.Current");
            Assert.AreEqual(5, effect.Width.Current, "Width.Current");
            Assert.AreEqual(BlendMode.Difference, effect.BlendMode, "BlendMode");
            Assert.AreEqual(3, effect.Shape, "Shape");
            Assert.AreEqual(false, effect.NoColor, "NoColor");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
            Assert.AreEqual(true, effect.NoColor2, "NoColor2");

            effect = obj.Effects[7] as GradationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(BlendMode.Screen, effect.BlendMode, "BlendMode");
            Assert.AreEqual(1, effect.Shape, "Shape");
            Assert.AreEqual(true, effect.NoColor, "NoColor");
            Assert.AreEqual(false, effect.NoColor2, "NoColor2");
            Assert.AreEqual(Color.FromArgb(153, 128, 77), effect.Color2, "Color2");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var gradation = new GradationEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = gradation.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[6] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "2");
        }
    }
}

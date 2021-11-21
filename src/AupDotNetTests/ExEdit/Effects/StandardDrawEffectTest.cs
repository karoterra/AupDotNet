using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class StandardDrawEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            var effect = obj.Effects[11] as StandardDrawEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(3, effect.X.Current, "X.Current");
            Assert.AreEqual(4, effect.Y.Current, "Y.Current");
            Assert.AreEqual(5, effect.Z.Current, "Z.Current");
            Assert.AreEqual(6, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(7, effect.Alpha.Current, "Alpha.Current");
            Assert.AreEqual(8, effect.Rotate.Current, "Rotate.Current");
            Assert.AreEqual(BlendMode.Normal, effect.BlendMode, "BlendMode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            effect = obj.Effects[11] as StandardDrawEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(BlendMode.Difference, effect.BlendMode, "BlendMode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var sd = new StandardDrawEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = sd.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 0);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 5);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "2");
        }
    }
}

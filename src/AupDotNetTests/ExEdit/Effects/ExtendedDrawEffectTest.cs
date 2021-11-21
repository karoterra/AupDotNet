using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ExtendedDrawEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            var effect = obj.Effects[11] as ExtendedDrawEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(3, effect.Z.Current, "Z.Current");
            Assert.AreEqual(4, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(5, effect.Alpha.Current, "Alpha.Current");
            Assert.AreEqual(6, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(7, effect.RotateX.Current, "RotateX.Current");
            Assert.AreEqual(8, effect.RotateY.Current, "RotateY.Current");
            Assert.AreEqual(9, effect.RotateZ.Current, "RotateZ.Current");
            Assert.AreEqual(10, effect.CenterX.Current, "CenterX.Current");
            Assert.AreEqual(11, effect.CenterY.Current, "CenterY.Current");
            Assert.AreEqual(12, effect.CenterZ.Current, "CenterZ.Current");
            Assert.AreEqual(BlendMode.Normal, effect.BlendMode, "BlendMode");
            Assert.AreEqual(false, effect.BackfaceCulling, "BackfaceCulling");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            effect = obj.Effects[11] as ExtendedDrawEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(BlendMode.Difference, effect.BlendMode, "BlendMode");
            Assert.AreEqual(true, effect.BackfaceCulling, "BackfaceCulling");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var ed = new ExtendedDrawEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = ed.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 10);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "2");
        }
    }
}

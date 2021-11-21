using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CameraControlEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            var effect = obj.Effects[0] as CameraControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(30, effect.Z.Current, "Z.Current");
            Assert.AreEqual(40, effect.TargetX.Current, "TargetX.Current");
            Assert.AreEqual(50, effect.TargetY.Current, "TargetY.Current");
            Assert.AreEqual(60, effect.TargetZ.Current, "TargetZ.Current");
            Assert.AreEqual(7, effect.TargetLayer.Current, "TargetLayer.Current");
            Assert.AreEqual(800, effect.Roll.Current, "Roll.Current");
            Assert.AreEqual(90, effect.ShallowFocus.Current, "ShallowFocus.Current");
            Assert.AreEqual(1000, effect.FieldOfView.Current, "FieldOfView.Current");
            Assert.AreEqual(true, effect.EnableZBufferShadow, "EnableZBufferShadow");
            Assert.AreEqual(0, effect.Range, "Range");

            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 25);
            effect = obj.Effects[0] as CameraControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.EnableZBufferShadow, "EnableZBufferShadow");
            Assert.AreEqual(5, effect.Range, "Range");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var camera = new CameraControlEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = camera.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 25);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

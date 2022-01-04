using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class GroupControlEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 10);
            var effect = obj.Effects[0] as GroupControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(30, effect.Z.Current, "Z.Current");
            Assert.AreEqual(400, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(500, effect.RotateX.Current, "RotateX.Current");
            Assert.AreEqual(600, effect.RotateY.Current, "RotateY.Current");
            Assert.AreEqual(700, effect.RotateZ.Current, "RotateZ.Current");
            Assert.AreEqual(true, effect.Inherit, "Inherit");
            Assert.AreEqual(false, effect.OnlySameGroup, "OnlySameGroup");
            Assert.AreEqual(0, effect.Range, "Range");

            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 15);
            effect = obj.Effects[0] as GroupControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Inherit, "Inherit");
            Assert.AreEqual(true, effect.OnlySameGroup, "OnlySameGroup");
            Assert.AreEqual(3, effect.Range, "Range");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var group = new GroupControlEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = group.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 10);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 15);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorShiftEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[3] as ColorShiftEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.Shift.Current, "Shift.Current");
            Assert.AreEqual(20, effect.Angle.Current, "Angle.Current");
            Assert.AreEqual(100, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(5, effect.ShiftType, "ShiftType");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var cs = new ColorShiftEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = cs.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
        }
    }
}

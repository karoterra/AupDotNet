using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ColorShiftFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 10);
            var effect = obj.Effects[6] as ColorShiftFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(5, effect.Shift.Current, "Shift.Current");
            Assert.AreEqual(20, effect.Angle.Current, "Angle.Current");
            Assert.AreEqual(100, effect.Intensity.Current, "Intensity.Current");
            Assert.AreEqual(2, effect.ShiftType, "ShiftType");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class DeinterlacingEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            var effect = obj.Effects[1] as DeinterlacingEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(0, effect.Mode, "Mode");

            effect = obj.Effects[2] as DeinterlacingEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Mode, "Mode");

            effect = obj.Effects[3] as DeinterlacingEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(2, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var deinterlacing = new DeinterlacingEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = deinterlacing.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "3");
        }
    }
}

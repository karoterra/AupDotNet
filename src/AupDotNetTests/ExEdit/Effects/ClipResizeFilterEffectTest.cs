using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ClipResizeFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[3] as ClipResizeFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Top.Current, "Top.Current");
            Assert.AreEqual(2, effect.Bottom.Current, "Bottom.Current");
            Assert.AreEqual(3, effect.Left.Current, "Left.Current");
            Assert.AreEqual(4, effect.Right.Current, "Right.Current");
            var data = new byte[12];
            Assert.IsTrue(effect.Data.SequenceEqual(data), "Data");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var cr = new ClipResizeFilterEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = cr.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class TimeControlEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 0);
            var effect = obj.Effects[0] as TimeControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Position.Current, "Position.Current");
            Assert.AreEqual(2, effect.Repeat.Current, "Repeat.Current");
            Assert.AreEqual(3, effect.DropFrame.Current, "DropFrame.Current");
            Assert.AreEqual(false, effect.FrameMode, "FrameMode");
            Assert.AreEqual(0, effect.Range, "Range");

            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 5);
            effect = obj.Effects[0] as TimeControlEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.FrameMode, "FrameMode");
            Assert.AreEqual(4, effect.Range, "Range");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var time = new TimeControlEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = time.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 0);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 4, 5);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

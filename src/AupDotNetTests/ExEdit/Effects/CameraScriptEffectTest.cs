using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CameraScriptEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            var effect = obj.Effects[2] as CameraScriptEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual("-- スクリプト(カメラ制御)\r\na = 123", effect.Text, "Text");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var script = new CameraScriptEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = script.DumpExtData();
            int len = script.Text.GetUTF16ByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "1");
        }
    }
}

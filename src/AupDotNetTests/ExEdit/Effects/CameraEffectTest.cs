using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CameraEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            var effect = obj.Effects[3] as CameraEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(0, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(0, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(0, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Root, effect.Directory, "Directory");
            Assert.AreEqual(2, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("", effect.BuildParams(), "Params to string");
            Assert.AreEqual(0, effect.Params.Count, "Params.Count");

            effect = obj.Effects[4] as CameraEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(300, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(400, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.All, effect.Directory, "Directory");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("カメラ効果サンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("color=0x112233", effect.BuildParams(), "Params to string");
            Assert.AreEqual("0x112233", effect.Params["color"], "Params[color]");

            effect = obj.Effects[5] as CameraEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(300, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(400, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(true, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Other, effect.Directory, "Directory");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("カメラ効果サンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("color=0x0a141e", effect.BuildParams(), "Params to string");
            Assert.AreEqual("0x0a141e", effect.Params["color"], "Params[color]");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var cam = new CameraEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = cam.DumpExtData();
            int len = 4 + cam.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = cam.BuildParams().GetSjisByteCount() + 1;
            Assert.IsTrue(data.Skip(260).Take(len).SequenceEqual(custom.Data.Skip(260).Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 4, 20);
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[5] as CustomEffect, "3");
        }
    }
}

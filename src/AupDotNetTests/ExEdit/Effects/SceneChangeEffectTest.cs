using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class SceneChangeEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            var effect = obj.Effects[0] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(0, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(false, effect.Reverse, "Reverse");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(1, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNull(effect.Params, "Params");
            Assert.AreEqual("*", effect.BuildParams(), "Params to string");

            effect = obj.Effects[1] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(true, effect.Reverse, "Reverse");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(21, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNull(effect.Params, "Params");
            Assert.AreEqual("*", effect.BuildParams(), "Params to string");

            effect = obj.Effects[2] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(0, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(false, effect.Reverse, "Reverse");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(1, effect.ScriptId, "ScriptId");
            Assert.AreEqual("ドア", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("", effect.BuildParams(), "Params to string");

            effect = obj.Effects[3] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(0, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(false, effect.Reverse, "Reverse");
            Assert.AreEqual(true, effect.Check0, "Check0");
            Assert.AreEqual(1, effect.ScriptId, "ScriptId");
            Assert.AreEqual("ドア", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("", effect.BuildParams(), "Params to string");

            effect = obj.Effects[4] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(0, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(false, effect.Reverse, "Reverse");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("sampleTransition", effect.Name, "Name");
            Assert.IsNull(effect.Params, "Params");
            Assert.AreEqual("*", effect.BuildParams(), "Params to string");

            effect = obj.Effects[5] as SceneChangeEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(false, effect.Reverse, "Reverse");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(2, effect.ScriptId, "ScriptId");
            Assert.AreEqual("シーンチェンジサンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("local size=100;local col=0xffffff;local fig=\"四角形\";", effect.BuildParams(), "Params to string");
            Assert.AreEqual("100", effect.Params["local size"], "Params[local size]");
            Assert.AreEqual("0xffffff", effect.Params["local col"], "Params[local col]");
            Assert.AreEqual("\"四角形\"", effect.Params["local fig"], "Params[local fig]");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var scn = new SceneChangeEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = scn.DumpExtData();
            int len = 4 + scn.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = scn.BuildParams().GetSjisByteCount() + 1;
            Assert.IsTrue(data.Skip(260).Take(len).SequenceEqual(custom.Data.Skip(260).Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 0);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "3");
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "4");
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "5");
            CheckDumpExtData(obj.Effects[5] as CustomEffect, "6");
        }
    }
}

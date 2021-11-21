using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class AnimationEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            var effect = obj.Effects[2] as AnimationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(50, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(9000, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(100, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Root, effect.Directory, "Directory");
            Assert.AreEqual(4, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("", effect.BuildParams(), "Params to string");

            effect = obj.Effects[3] as AnimationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.All, effect.Directory, "Directory");
            Assert.AreEqual(4, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("", effect.BuildParams(), "Params to string");

            effect = obj.Effects[4] as AnimationEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(300, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(400, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Other, effect.Directory, "Directory");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("アニメーション効果サンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("size=100;col=0xffffff;fig=\"四角形\";", effect.BuildParams(), "Params to string");
            Assert.AreEqual("100", effect.Params["size"], "Params[size]");
            Assert.AreEqual("0xffffff", effect.Params["col"], "Params[col]");
            Assert.AreEqual("\"四角形\"", effect.Params["fig"], "Params[fig]");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var anm = new AnimationEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = anm.DumpExtData();
            int len = 4 + anm.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = anm.BuildParams().GetSjisByteCount() + 1;
            Assert.IsTrue(data.Skip(260).Take(len).SequenceEqual(custom.Data.Skip(260).Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[3] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[4] as CustomEffect, "3");
        }
    }
}

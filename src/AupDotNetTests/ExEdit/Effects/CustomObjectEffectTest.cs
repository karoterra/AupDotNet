using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class CustomObjectEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 95);
            var effect = obj.Effects[0] as CustomObjectEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(500, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(-12000, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(3000, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(0, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Root, effect.Directory, "Directory");
            Assert.AreEqual(14, effect.ScriptId, "ScriptId");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("col=0x005ab4;hw=5;cl=300;hs=20;hb=5;res=50;adjust=25;", effect.BuildParams(), "Params to string");
            Assert.AreEqual("0x005ab4", effect.Params["col"], "Params[col]");
            Assert.AreEqual("5", effect.Params["hw"], "Params[hw]");
            Assert.AreEqual("300", effect.Params["cl"], "Params[cl]");
            Assert.AreEqual("20", effect.Params["hs"], "Params[hs]");
            Assert.AreEqual("5", effect.Params["hb"], "Params[hb]");
            Assert.AreEqual("50", effect.Params["res"], "Params[res]");
            Assert.AreEqual("25", effect.Params["adjust"], "Params[adjust]");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 100);
            effect = obj.Effects[0] as CustomObjectEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(300, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(400, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(false, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.All, effect.Directory, "Directory");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("カスタムサンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual("file=", effect.BuildParams(), "Params to string");
            Assert.AreEqual("", effect.Params["file"], "Params[file]");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 105);
            effect = obj.Effects[0] as CustomObjectEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(100, effect.Trackbars[0].Current, "Trackbars[0].Current");
            Assert.AreEqual(200, effect.Trackbars[1].Current, "Trackbars[1].Current");
            Assert.AreEqual(300, effect.Trackbars[2].Current, "Trackbars[2].Current");
            Assert.AreEqual(400, effect.Trackbars[3].Current, "Trackbars[3].Current");
            Assert.AreEqual(true, effect.Check0, "Check0");
            Assert.AreEqual(ScriptDirectory.Other, effect.Directory, "Directory");
            Assert.AreEqual(0, effect.ScriptId, "ScriptId");
            Assert.AreEqual("カスタムサンプル", effect.Name, "Name");
            Assert.IsNotNull(effect.Params, "Params");
            Assert.AreEqual(@"file=""D:\\TestData\\サンプル.png""", effect.BuildParams(), "Params to string");
            Assert.AreEqual(@"""D:\\TestData\\サンプル.png""", effect.Params["file"], "Params[file]");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var obj = new CustomObjectEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = obj.DumpExtData();
            int len = 4 + obj.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = obj.BuildParams().GetSjisByteCount() + 1;
            Assert.IsTrue(data.Skip(260).Take(len).SequenceEqual(custom.Data.Skip(260).Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 95);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 100);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 105);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "3");
        }
    }
}

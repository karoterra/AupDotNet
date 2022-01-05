using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class DisplacementEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            var effect = obj.Effects[7] as DisplacementEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.Param0.Current, "Param0.Current");
            Assert.AreEqual(20, effect.Param1.Current, "Param1.Current");
            Assert.AreEqual(30, effect.X.Current, "X.Current");
            Assert.AreEqual(40, effect.Y.Current, "Y.Current");
            Assert.AreEqual(500, effect.Rotate.Current, "Rotate.Current");
            Assert.AreEqual(200, effect.Size.Current, "Size.Current");
            Assert.AreEqual(60, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(5, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(false, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Circle, effect.FigureType, "FigureType");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.BuiltIn, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");
            Assert.AreEqual(DisplacementCalc.Move, effect.Calc, "Calc");

            effect = obj.Effects[8] as DisplacementEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual("sampleFigure", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Figure, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");
            Assert.AreEqual(DisplacementCalc.Scale, effect.Calc, "Calc");

            effect = obj.Effects[9] as DisplacementEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(@"*D:\TestData\sample.bmp", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.File, effect.NameType, "NameType");
            Assert.AreEqual(@"D:\TestData\sample.bmp", effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");
            Assert.AreEqual(DisplacementCalc.Rotate, effect.Calc, "Calc");

            effect = obj.Effects[10] as DisplacementEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(":1", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Scene, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(1, effect.Scene, "Scene");
            Assert.AreEqual(1, effect.Mode, "Mode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            effect = obj.Effects[1] as DisplacementEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(":49", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Scene, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(49, effect.Scene, "Scene");
            Assert.AreEqual(2, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var disp = new DisplacementEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = disp.DumpExtData();
            var len = 4 + disp.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = 4 + DisplacementEffect.MaxNameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            CheckDumpExtData(obj.Effects[7] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "3");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "4");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "5");
        }

        [TestMethod]
        public void Test_Name()
        {
            var disp = new DisplacementEffect();

            disp.Name = string.Empty;
            Assert.AreEqual(FigureNameType.BuiltIn, disp.NameType);
            Assert.IsNull(disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = "figure";
            Assert.AreEqual(FigureNameType.Figure, disp.NameType);
            Assert.IsNull(disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = "*";
            Assert.AreEqual(FigureNameType.File, disp.NameType);
            Assert.AreEqual(string.Empty, disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = "*figure";
            Assert.AreEqual(FigureNameType.File, disp.NameType);
            Assert.AreEqual("figure", disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = ":";
            Assert.AreEqual(FigureNameType.Scene, disp.NameType);
            Assert.IsNull(disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = ":figure";
            Assert.AreEqual(FigureNameType.Scene, disp.NameType);
            Assert.IsNull(disp.Filename);
            Assert.IsNull(disp.Scene);

            disp.Name = ":1";
            Assert.AreEqual(FigureNameType.Scene, disp.NameType);
            Assert.IsNull(disp.Filename);
            Assert.AreEqual(1, disp.Scene);

            disp.Filename = null;
            Assert.AreEqual(FigureNameType.File, disp.NameType);
            Assert.AreEqual("*", disp.Name);
            Assert.IsNull(disp.Scene);

            disp.Filename = "figure";
            Assert.AreEqual(FigureNameType.File, disp.NameType);
            Assert.AreEqual("*figure", disp.Name);
            Assert.IsNull(disp.Scene);

            disp.Scene = null;
            Assert.AreEqual(FigureNameType.Scene, disp.NameType);
            Assert.AreEqual(":0", disp.Name);
            Assert.IsNull(disp.Filename);

            disp.Scene = 1;
            Assert.AreEqual(FigureNameType.Scene, disp.NameType);
            Assert.AreEqual(":1", disp.Name);
            Assert.IsNull(disp.Filename);
        }
    }
}

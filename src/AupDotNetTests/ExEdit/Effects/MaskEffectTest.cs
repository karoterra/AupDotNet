using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class MaskEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            var effect = obj.Effects[8] as MaskEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(300, effect.Rotate.Current, "Rotate.Current");
            Assert.AreEqual(100, effect.Size.Current, "Size.Current");
            Assert.AreEqual(40, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(1, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(true, effect.Invert, "Invert");
            Assert.AreEqual(false, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Square, effect.FigureType, "FigureType");
            Assert.AreEqual("", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.BuiltIn, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");

            effect = obj.Effects[9] as MaskEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Invert, "Invert");
            Assert.AreEqual(true, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual("sampleFigure", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Figure, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");

            effect = obj.Effects[10] as MaskEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Invert, "Invert");
            Assert.AreEqual(false, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(@"*D:\TestData\sample.bmp", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.File, effect.NameType, "NameType");
            Assert.AreEqual(@"D:\TestData\sample.bmp", effect.Filename, "Filename");
            Assert.AreEqual(null, effect.Scene, "Scene");
            Assert.AreEqual(0, effect.Mode, "Mode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            effect = obj.Effects[1] as MaskEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Invert, "Invert");
            Assert.AreEqual(true, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(":1", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Scene, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(1, effect.Scene, "Scene");
            Assert.AreEqual(1, effect.Mode, "Mode");

            effect = obj.Effects[2] as MaskEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Invert, "Invert");
            Assert.AreEqual(false, effect.Fit, "Fit");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(":0", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Scene, effect.NameType, "NameType");
            Assert.AreEqual(null, effect.Filename, "Filename");
            Assert.AreEqual(0, effect.Scene, "Scene");
            Assert.AreEqual(2, effect.Mode, "Mode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var mask = new MaskEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = mask.DumpExtData();
            var len = 4 + mask.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = 4 + mask.MaxNameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            CheckDumpExtData(obj.Effects[8] as CustomEffect, "1");
            CheckDumpExtData(obj.Effects[9] as CustomEffect, "2");
            CheckDumpExtData(obj.Effects[10] as CustomEffect, "3");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            CheckDumpExtData(obj.Effects[1] as CustomEffect, "4");
            CheckDumpExtData(obj.Effects[2] as CustomEffect, "5");
        }

        [TestMethod]
        public void Test_Name()
        {
            var mask = new MaskEffect();

            mask.Name = string.Empty;
            Assert.AreEqual(FigureNameType.BuiltIn, mask.NameType);
            Assert.IsNull(mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = "figure";
            Assert.AreEqual(FigureNameType.Figure, mask.NameType);
            Assert.IsNull(mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = "*";
            Assert.AreEqual(FigureNameType.File, mask.NameType);
            Assert.AreEqual(string.Empty, mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = "*figure";
            Assert.AreEqual(FigureNameType.File, mask.NameType);
            Assert.AreEqual("figure", mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = ":";
            Assert.AreEqual(FigureNameType.Scene, mask.NameType);
            Assert.IsNull(mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = ":figure";
            Assert.AreEqual(FigureNameType.Scene, mask.NameType);
            Assert.IsNull(mask.Filename);
            Assert.IsNull(mask.Scene);

            mask.Name = ":1";
            Assert.AreEqual(FigureNameType.Scene, mask.NameType);
            Assert.IsNull(mask.Filename);
            Assert.AreEqual(1, mask.Scene);

            mask.Filename = null;
            Assert.AreEqual(FigureNameType.File, mask.NameType);
            Assert.AreEqual("*", mask.Name);
            Assert.IsNull(mask.Scene);

            mask.Filename = "figure";
            Assert.AreEqual(FigureNameType.File, mask.NameType);
            Assert.AreEqual("*figure", mask.Name);
            Assert.IsNull(mask.Scene);

            mask.Scene = null;
            Assert.AreEqual(FigureNameType.Scene, mask.NameType);
            Assert.AreEqual(":0", mask.Name);
            Assert.IsNull(mask.Filename);

            mask.Scene = 1;
            Assert.AreEqual(FigureNameType.Scene, mask.NameType);
            Assert.AreEqual(":1", mask.Name);
            Assert.IsNull(mask.Filename);
        }
    }
}

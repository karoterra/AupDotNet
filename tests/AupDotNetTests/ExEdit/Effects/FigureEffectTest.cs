using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class FigureEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[0] as FigureEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.Size.Current, "Size.Current");
            Assert.AreEqual(20, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(3, effect.LineWidth.Current, "LineWidth.Current");
            Assert.AreEqual(FigureType.Circle, effect.FigureType, "FigureType");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
            Assert.AreEqual(@"", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.BuiltIn, effect.NameType, "NameType");
            Assert.IsNull(effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            effect = obj.Effects[0] as FigureEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(Color.FromArgb(40, 50, 60), effect.Color, "Color");
            Assert.AreEqual(@"sampleFigure", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.Figure, effect.NameType, "NameType");
            Assert.IsNull(effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            effect = obj.Effects[0] as FigureEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
            Assert.AreEqual(@"*D:\TestData\サンプル.bmp", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.File, effect.NameType, "NameType");
            Assert.AreEqual(@"D:\TestData\サンプル.bmp", effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 55);
            effect = obj.Effects[0] as FigureEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
            Assert.AreEqual(@"*", effect.Name, "Name");
            Assert.AreEqual(FigureNameType.File, effect.NameType, "NameType");
            Assert.AreEqual(@"", effect.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var fig = new FigureEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = fig.DumpExtData();
            int len = 8 + fig.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "3");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 55);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "4");
        }

        [TestMethod]
        public void Test_Name()
        {
            var fig = new FigureEffect();

            fig.Name = string.Empty;
            Assert.AreEqual(FigureNameType.BuiltIn, fig.NameType);
            Assert.IsNull(fig.Filename);

            fig.Name = "figure";
            Assert.AreEqual(FigureNameType.Figure, fig.NameType);
            Assert.IsNull(fig.Filename);

            fig.Name = "*";
            Assert.AreEqual(FigureNameType.File, fig.NameType);
            Assert.AreEqual(string.Empty, fig.Filename);

            fig.Name = "*figure";
            Assert.AreEqual(FigureNameType.File, fig.NameType);
            Assert.AreEqual("figure", fig.Filename);

            fig.Name = ":";
            Assert.AreEqual(FigureNameType.Figure, fig.NameType);
            Assert.IsNull(fig.Filename);

            fig.Name = ":figure";
            Assert.AreEqual(FigureNameType.Figure, fig.NameType);
            Assert.IsNull(fig.Filename);

            fig.Name = ":1";
            Assert.AreEqual(FigureNameType.Figure, fig.NameType);
            Assert.IsNull(fig.Filename);

            fig.Filename = null;
            Assert.AreEqual(FigureNameType.File, fig.NameType);
            Assert.AreEqual("*", fig.Name);

            fig.Filename = "figure";
            Assert.AreEqual(FigureNameType.File, fig.NameType);
            Assert.AreEqual("*figure", fig.Name);
        }
    }
}

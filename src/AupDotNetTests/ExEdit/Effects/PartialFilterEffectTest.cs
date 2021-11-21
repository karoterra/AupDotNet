using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;
using Karoterra.AupDotNet.Extensions;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class PartialFilterEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            var effect = obj.Effects[0] as PartialFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(10, effect.X.Current, "X.Current");
            Assert.AreEqual(20, effect.Y.Current, "Y.Current");
            Assert.AreEqual(300, effect.Rotate.Current, "Rotate.Current");
            Assert.AreEqual(4, effect.Size.Current, "Size.Current");
            Assert.AreEqual(50, effect.AspectRatio.Current, "AspectRatio.Current");
            Assert.AreEqual(6, effect.Blur.Current, "Blur.Current");
            Assert.AreEqual(false, effect.Invert, "Invert");
            Assert.AreEqual(FigureType.Square, effect.FigureType, "FigureType");
            Assert.AreEqual(FigureNameType.BuiltIn, effect.NameType, "NameType");
            Assert.AreEqual(@"", effect.Name, "Name");
            Assert.IsNull(effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 20);
            effect = obj.Effects[0] as PartialFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.Invert, "Invert");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(FigureNameType.Figure, effect.NameType, "NameType");
            Assert.AreEqual(@"sampleFigure", effect.Name, "Name");
            Assert.IsNull(effect.Filename, "Filename");

            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 25);
            effect = obj.Effects[0] as PartialFilterEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Invert, "Invert");
            Assert.AreEqual(FigureType.Background, effect.FigureType, "FigureType");
            Assert.AreEqual(FigureNameType.File, effect.NameType, "NameType");
            Assert.AreEqual(@"*D:\TestData\サンプル.png", effect.Name, "Name");
            Assert.AreEqual(@"D:\TestData\サンプル.png", effect.Filename, "Filename");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var pf = new PartialFilterEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = pf.DumpExtData();
            int len = 4 + pf.Name.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 2, 15);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 20);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
            obj = ExeditTestUtil.GetObject(exedit, 0, 2, 25);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "3");
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

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
    public class TextEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            var effect = obj.Effects[0] as TextEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(34, effect.Size.Current, "Size.Current");
            Assert.AreEqual(10, effect.Speed.Current, "Speed.Current");
            Assert.AreEqual(true, effect.AutoScroll, "AutoScroll");
            Assert.AreEqual(false, effect.Split, "Split");
            Assert.AreEqual(false, effect.DisplayOnMoving, "DisplayOnMoving");
            Assert.AreEqual(false, effect.Bold, "Bold");
            Assert.AreEqual(false, effect.Italic, "Italic");
            Assert.AreEqual(TextType.Normal, effect.TextType, "TextType");
            Assert.AreEqual(TextAlign.TopLeft, effect.Align, "Align");
            Assert.AreEqual(true, effect.AutoAdjust, "AutoAdjust");
            Assert.AreEqual(false, effect.Soft, "Soft");
            Assert.AreEqual(false, effect.Monospace, "Monospace");
            Assert.AreEqual(false, effect.Precision, "Precision");
            Assert.AreEqual(100, effect.SpacingX, "SpacingX");
            Assert.AreEqual(-100, effect.SpacingY, "SpacingY");
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
            Assert.AreEqual(Color.FromArgb(0, 0, 0), effect.Color2, "Color2");
            Assert.AreEqual("MS UI Gothic", effect.Font, "Font");
            Assert.AreEqual("あいうえお\r\nABCdef", effect.Text, "Text");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            effect = obj.Effects[0] as TextEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.AutoScroll, "AutoScroll");
            Assert.AreEqual(true, effect.Split, "Split");
            Assert.AreEqual(false, effect.DisplayOnMoving, "DisplayOnMoving");
            Assert.AreEqual(false, effect.Bold, "Bold");
            Assert.AreEqual(false, effect.Italic, "Italic");
            Assert.AreEqual(TextType.Shadow, effect.TextType, "TextType");
            Assert.AreEqual(TextAlign.MiddleCenter, effect.Align, "Align");
            Assert.AreEqual(false, effect.AutoAdjust, "AutoAdjust");
            Assert.AreEqual(true, effect.Soft, "Soft");
            Assert.AreEqual(false, effect.Monospace, "Monospace");
            Assert.AreEqual(false, effect.Precision, "Precision");
            Assert.AreEqual(0, effect.SpacingX, "SpacingX");
            Assert.AreEqual(0, effect.SpacingY, "SpacingY");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
            Assert.AreEqual(Color.FromArgb(40, 50, 60), effect.Color2, "Color2");
            Assert.AreEqual("游ゴシック Light", effect.Font, "Font");
            Assert.AreEqual("", effect.Text, "Text");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            effect = obj.Effects[0] as TextEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.AutoScroll, "AutoScroll");
            Assert.AreEqual(true, effect.Split, "Split");
            Assert.AreEqual(true, effect.DisplayOnMoving, "DisplayOnMoving");
            Assert.AreEqual(false, effect.Bold, "Bold");
            Assert.AreEqual(false, effect.Italic, "Italic");
            Assert.AreEqual(TextType.LightShadow, effect.TextType, "TextType");
            Assert.AreEqual(TextAlign.BottomRight, effect.Align, "Align");
            Assert.AreEqual(false, effect.AutoAdjust, "AutoAdjust");
            Assert.AreEqual(false, effect.Soft, "Soft");
            Assert.AreEqual(false, effect.Monospace, "Monospace");
            Assert.AreEqual(true, effect.Precision, "Precision");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            effect = obj.Effects[0] as TextEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.AutoScroll, "AutoScroll");
            Assert.AreEqual(false, effect.Split, "Split");
            Assert.AreEqual(false, effect.DisplayOnMoving, "DisplayOnMoving");
            Assert.AreEqual(true, effect.Bold, "Bold");
            Assert.AreEqual(false, effect.Italic, "Italic");
            Assert.AreEqual(TextType.Outline, effect.TextType, "TextType");
            Assert.AreEqual(TextAlign.VerticalRightTop, effect.Align, "Align");
            Assert.AreEqual(false, effect.AutoAdjust, "AutoAdjust");
            Assert.AreEqual(false, effect.Soft, "Soft");
            Assert.AreEqual(true, effect.Monospace, "Monospace");
            Assert.AreEqual(false, effect.Precision, "Precision");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            effect = obj.Effects[0] as TextEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.AutoScroll, "AutoScroll");
            Assert.AreEqual(false, effect.Split, "Split");
            Assert.AreEqual(false, effect.DisplayOnMoving, "DisplayOnMoving");
            Assert.AreEqual(false, effect.Bold, "Bold");
            Assert.AreEqual(true, effect.Italic, "Italic");
            Assert.AreEqual(TextType.ThinOutline, effect.TextType, "TextType");
            Assert.AreEqual(TextAlign.VerticalLeftBottom, effect.Align, "Align");
            Assert.AreEqual(false, effect.AutoAdjust, "AutoAdjust");
            Assert.AreEqual(true, effect.Soft, "Soft");
            Assert.AreEqual(false, effect.Monospace, "Monospace");
            Assert.AreEqual(true, effect.Precision, "Precision");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var text = new TextEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = text.DumpExtData();
            var len = 16 + text.Font.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = text.Text.GetUTF16ByteCount() + 1;
            Assert.IsTrue(data.Skip(48).Take(len).SequenceEqual(custom.Data.Skip(48).Take(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 15);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 20);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 25);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "3");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 30);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "4");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 35);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "5");
        }
    }
}

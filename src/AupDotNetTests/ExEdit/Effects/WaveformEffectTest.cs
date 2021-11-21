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
    public class WaveformEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 70);
            var effect = obj.Effects[0] as WaveformEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(640, effect.Width.Current, "Width.Current");
            Assert.AreEqual(240, effect.Height.Current, "Height.Current");
            Assert.AreEqual(1000, effect.Volume.Current, "Volume.Current");
            Assert.AreEqual(1, effect.Position.Current, "Position.Current");
            Assert.AreEqual(false, effect.ReferScene, "ReferScene");
            Assert.AreEqual(@"D:\TestData\sample.wav", effect.Filename, "Filename");
            Assert.AreEqual(4, effect.PresetType, "PresetType");
            Assert.AreEqual(1, effect.Mode, "Mode");
            Assert.AreEqual(36, effect.ResW, "ResW");
            Assert.AreEqual(72, effect.ResH, "ResH");
            Assert.AreEqual(75, effect.PadW, "PadW");
            Assert.AreEqual(2, effect.PadH, "PadH");
            Assert.AreEqual(Color.FromArgb(10, 20, 30), effect.Color, "Color");
            Assert.AreEqual(0, effect.SampleN, "SampleN");
            Assert.AreEqual(false, effect.Mirror, "Mirror");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 75);
            effect = obj.Effects[0] as WaveformEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(true, effect.ReferScene, "ReferScene");
            Assert.AreEqual(@"", effect.Filename, "Filename");
            Assert.AreEqual(3, effect.PresetType, "PresetType");
            Assert.AreEqual(0, effect.Mode, "Mode");
            Assert.AreEqual(1024, effect.ResW, "ResW");
            Assert.AreEqual(4096, effect.ResH, "ResH");
            Assert.AreEqual(10, effect.PadW, "PadW");
            Assert.AreEqual(20, effect.PadH, "PadH");
            Assert.AreEqual(Color.FromArgb(255, 255, 255), effect.Color, "Color");
            Assert.AreEqual(0, effect.SampleN, "SampleN");
            Assert.AreEqual(true, effect.Mirror, "Mirror");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var wave = new WaveformEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = wave.DumpExtData();
            int len = wave.Filename.GetSjisByteCount() + 1;
            Assert.IsTrue(data.Take(len).SequenceEqual(custom.Data.Take(len)), message);
            len = wave.MaxFilenameLength;
            Assert.IsTrue(data.Skip(len).SequenceEqual(custom.Data.Skip(len)), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 70);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 75);
            CheckDumpExtData(obj.Effects[0] as CustomEffect, "2");
        }
    }
}

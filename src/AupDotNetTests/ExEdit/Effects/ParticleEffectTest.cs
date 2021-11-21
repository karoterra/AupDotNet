using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Karoterra.AupDotNet;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit.Effects
{
    [TestClass]
    public class ParticleEffectTest
    {
        [TestMethod]
        public void Test_Read()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup);

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            var effect = obj.Effects[11] as ParticleEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(1, effect.X.Current, "X.Current");
            Assert.AreEqual(2, effect.Y.Current, "Y.Current");
            Assert.AreEqual(3, effect.Z.Current, "Z.Current");
            Assert.AreEqual(4, effect.Frequency.Current, "Frequency.Current");
            Assert.AreEqual(5, effect.Speed.Current, "Speed.Current");
            Assert.AreEqual(6, effect.Acceleration.Current, "Acceleration.Current");
            Assert.AreEqual(7, effect.Direction.Current, "Direction.Current");
            Assert.AreEqual(8, effect.Diffusion.Current, "Diffusion.Current");
            Assert.AreEqual(9, effect.Alpha.Current, "Alpha.Current");
            Assert.AreEqual(10, effect.Fading.Current, "Fading.Current");
            Assert.AreEqual(11, effect.Zoom.Current, "Zoom.Current");
            Assert.AreEqual(12, effect.Expanding.Current, "Expanding.Current");
            Assert.AreEqual(13, effect.Rotate.Current, "Rotate.Current");
            Assert.AreEqual(14, effect.Revolution.Current, "Revolution.Current");
            Assert.AreEqual(15, effect.Gravity.Current, "Gravity.Current");
            Assert.AreEqual(16, effect.Duration.Current, "Duration.Current");
            Assert.AreEqual(true, effect.Tail, "Tail");
            Assert.AreEqual(false, effect.EmittingFromTrace, "EmittingFromTrace");
            Assert.AreEqual(false, effect.RandomRotation, "RandomRotation");
            Assert.AreEqual(false, effect.Adjust, "Adjust");
            Assert.AreEqual(BlendMode.Normal, effect.BlendMode, "BlendMode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            effect = obj.Effects[11] as ParticleEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Tail, "Tail");
            Assert.AreEqual(true, effect.EmittingFromTrace, "EmittingFromTrace");
            Assert.AreEqual(false, effect.RandomRotation, "RandomRotation");
            Assert.AreEqual(false, effect.Adjust, "Adjust");
            Assert.AreEqual(BlendMode.Difference, effect.BlendMode, "BlendMode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            effect = obj.Effects.Last() as ParticleEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Tail, "Tail");
            Assert.AreEqual(false, effect.EmittingFromTrace, "EmittingFromTrace");
            Assert.AreEqual(true, effect.RandomRotation, "RandomRotation");
            Assert.AreEqual(false, effect.Adjust, "Adjust");
            Assert.AreEqual(BlendMode.Overlay, effect.BlendMode, "BlendMode");

            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 55);
            effect = obj.Effects.Last() as ParticleEffect;
            Assert.IsNotNull(effect);
            Assert.AreEqual(false, effect.Tail, "Tail");
            Assert.AreEqual(false, effect.EmittingFromTrace, "EmittingFromTrace");
            Assert.AreEqual(false, effect.RandomRotation, "RandomRotation");
            Assert.AreEqual(true, effect.Adjust, "Adjust");
            Assert.AreEqual(BlendMode.Luminance, effect.BlendMode, "BlendMode");
        }

        public void CheckDumpExtData(CustomEffect custom, string message)
        {
            var particle = new ParticleEffect(custom.Trackbars.ToArray(), custom.Checkboxes, custom.Data);
            var data = particle.DumpExtData();
            Assert.IsTrue(data.SequenceEqual(custom.Data), message);
        }

        [TestMethod]
        public void Test_DumpExtData()
        {
            AviUtlProject aup = new AviUtlProject(@"TestData\Exedit\EffectSet01.aup");
            ExEditProject exedit = ExeditTestUtil.GetExEdit(aup, new CustomEffectFactory());

            var obj = ExeditTestUtil.GetObject(exedit, 0, 0, 40);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "1");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 45);
            CheckDumpExtData(obj.Effects[11] as CustomEffect, "2");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 50);
            CheckDumpExtData(obj.Effects.Last() as CustomEffect, "3");
            obj = ExeditTestUtil.GetObject(exedit, 0, 0, 55);
            CheckDumpExtData(obj.Effects.Last() as CustomEffect, "4");
        }
    }
}

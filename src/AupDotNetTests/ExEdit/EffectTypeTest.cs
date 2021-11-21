using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Karoterra.AupDotNet.ExEdit;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace AupDotNetTests.ExEdit
{
    [TestClass]
    public class EffectTypeTest
    {
        public static void ValidateEffectTypeId(Effect effect, EffectTypeId id)
        {
            Assert.IsTrue(effect.Type.Equals(EffectType.Defaults[(int)id]), effect.GetType().Name);
        }

        [TestMethod]
        public void Test_EffectTypeId()
        {
            ValidateEffectTypeId(new VideoFileEffect(), EffectTypeId.VideoFile);
            ValidateEffectTypeId(new ImageFileEffect(), EffectTypeId.ImageFile);
            ValidateEffectTypeId(new AudioFileEffect(), EffectTypeId.AudioFile);
            ValidateEffectTypeId(new TextEffect(), EffectTypeId.Text);
            ValidateEffectTypeId(new FigureEffect(), EffectTypeId.Figure);
            ValidateEffectTypeId(new FrameBufferEffect(), EffectTypeId.FrameBuffer);
            ValidateEffectTypeId(new WaveformEffect(), EffectTypeId.Waveform);
            ValidateEffectTypeId(new SceneEffect(), EffectTypeId.Scene);
            ValidateEffectTypeId(new SceneAudioEffect(), EffectTypeId.SceneAudio);
            ValidateEffectTypeId(new PreviousObjectEffect(), EffectTypeId.PreviousObject);
            ValidateEffectTypeId(new StandardDrawEffect(), EffectTypeId.StandardDraw);
            ValidateEffectTypeId(new ExtendedDrawEffect(), EffectTypeId.ExtendedDraw);
            ValidateEffectTypeId(new StandardPlaybackEffect(), EffectTypeId.StandardPlayback);
            ValidateEffectTypeId(new ParticleEffect(), EffectTypeId.Particle);
            ValidateEffectTypeId(new SceneChangeEffect(), EffectTypeId.SceneChange);
            ValidateEffectTypeId(new ColorCorrectionEffect(), EffectTypeId.ColorCorrection);
            ValidateEffectTypeId(new ColorCorrectionFilterEffect(), EffectTypeId.ColorCorrectionFilter);
            ValidateEffectTypeId(new ClippingEffect(), EffectTypeId.Clipping);
            ValidateEffectTypeId(new BlurEffect(), EffectTypeId.Blur);
            ValidateEffectTypeId(new BorderBlurEffect(), EffectTypeId.BorderBlur);
            ValidateEffectTypeId(new BlurFilterEffect(), EffectTypeId.BlurFilter);
            ValidateEffectTypeId(new MosaicEffect(), EffectTypeId.Mosaic);
            ValidateEffectTypeId(new MosaicFilterEffect(), EffectTypeId.MosaicFilter);
            ValidateEffectTypeId(new EmissionEffect(), EffectTypeId.Emission);
            ValidateEffectTypeId(new EmissionFilterEffect(), EffectTypeId.EmissionFilter);
            ValidateEffectTypeId(new FlashEffect(), EffectTypeId.Flash);
            ValidateEffectTypeId(new DiffuseEffect(), EffectTypeId.Diffuse);
            ValidateEffectTypeId(new DiffuseFilterEffect(), EffectTypeId.DiffuseFilter);
            ValidateEffectTypeId(new GlowEffect(), EffectTypeId.Glow);
            ValidateEffectTypeId(new GlowFilterEffect(), EffectTypeId.GlowFilter);
            ValidateEffectTypeId(new ChromaKeyEffect(), EffectTypeId.ChromaKey);
            ValidateEffectTypeId(new ColorKeyEffect(), EffectTypeId.ColorKey);
            ValidateEffectTypeId(new LuminanceKeyEffect(), EffectTypeId.LuminanceKey);
            ValidateEffectTypeId(new LightEffect(), EffectTypeId.Light);
            ValidateEffectTypeId(new ShadowEffect(), EffectTypeId.Shadow);
            ValidateEffectTypeId(new BorderEffect(), EffectTypeId.Border);
            ValidateEffectTypeId(new BevelEffect(), EffectTypeId.Bevel);
            ValidateEffectTypeId(new EdgeExtractionEffect(), EffectTypeId.EdgeExtraction);
            ValidateEffectTypeId(new SharpenEffect(), EffectTypeId.Sharpen);
            ValidateEffectTypeId(new FadeEffect(), EffectTypeId.Fade);
            ValidateEffectTypeId(new WipeEffect(), EffectTypeId.Wipe);
            ValidateEffectTypeId(new MaskEffect(), EffectTypeId.Mask);
            ValidateEffectTypeId(new DiagonalClippingEffect(), EffectTypeId.DiagonalClipping);
            ValidateEffectTypeId(new RadialBlurEffect(), EffectTypeId.RadialBlur);
            ValidateEffectTypeId(new RadialBlurFilterEffect(), EffectTypeId.RadialBlurFilter);
            ValidateEffectTypeId(new DirectionalBlurEffect(), EffectTypeId.DirectionalBlur);
            ValidateEffectTypeId(new DirectionalBlurFilterEffect(), EffectTypeId.DirectionalBlurFilter);
            ValidateEffectTypeId(new LensBlurEffect(), EffectTypeId.LensBlur);
            ValidateEffectTypeId(new LensBlurFilterEffect(), EffectTypeId.LensBlurFilter);
            ValidateEffectTypeId(new MotionBlurEffect(), EffectTypeId.MotionBlur);
            ValidateEffectTypeId(new MotionBlurFilterEffect(), EffectTypeId.MotionBlurFilter);
            ValidateEffectTypeId(new PositionEffect(), EffectTypeId.Position);
            ValidateEffectTypeId(new ZoomEffect(), EffectTypeId.Zoom);
            ValidateEffectTypeId(new TransparencyEffect(), EffectTypeId.Transparency);
            ValidateEffectTypeId(new RotationEffect(), EffectTypeId.Rotation);
            ValidateEffectTypeId(new AreaExpansionEffect(), EffectTypeId.AreaExpansion);
            ValidateEffectTypeId(new ResizeEffect(), EffectTypeId.Resize);
            ValidateEffectTypeId(new Rotation90Effect(), EffectTypeId.Rotation90);
            ValidateEffectTypeId(new VibrationEffect(), EffectTypeId.Vibration);
            ValidateEffectTypeId(new VibrationFilterEffect(), EffectTypeId.VibrationFilter);
            ValidateEffectTypeId(new InversionEffect(), EffectTypeId.Inversion);
            ValidateEffectTypeId(new InversionFilterEffect(), EffectTypeId.InversionFilter);
            ValidateEffectTypeId(new MirrorEffect(), EffectTypeId.Mirror);
            ValidateEffectTypeId(new RasterEffect(), EffectTypeId.Raster);
            ValidateEffectTypeId(new RasterFilterEffect(), EffectTypeId.RasterFilter);
            ValidateEffectTypeId(new RippleEffect(), EffectTypeId.Ripple);
            ValidateEffectTypeId(new ImageLoopEffect(), EffectTypeId.ImageLoop);
            ValidateEffectTypeId(new ImageLoopFilterEffect(), EffectTypeId.ImageLoopFilter);
            ValidateEffectTypeId(new PolarTransformEffect(), EffectTypeId.PolarTransform);
            ValidateEffectTypeId(new DisplacementEffect(), EffectTypeId.Displacement);
            ValidateEffectTypeId(new NoiseEffect(), EffectTypeId.Noise);
            ValidateEffectTypeId(new ColorShiftEffect(), EffectTypeId.ColorShift);
            ValidateEffectTypeId(new ColorShiftFilterEffect(), EffectTypeId.ColorShiftFilter);
            ValidateEffectTypeId(new MonochromaticEffect(), EffectTypeId.Monochromatic);
            ValidateEffectTypeId(new MonochromaticFilterEffect(), EffectTypeId.MonochromaticFilter);
            ValidateEffectTypeId(new GradationEffect(), EffectTypeId.Gradation);
            ValidateEffectTypeId(new ColorSettingExEffect(), EffectTypeId.ColorSettingEx);
            ValidateEffectTypeId(new ColorSettingExFilterEffect(), EffectTypeId.ColorSettingExFilter);
            ValidateEffectTypeId(new GamutConversionEffect(), EffectTypeId.GamutConversion);
            ValidateEffectTypeId(new AnimationEffect(), EffectTypeId.AnimationEffect);
            ValidateEffectTypeId(new CustomObjectEffect(), EffectTypeId.CustomObject);
            ValidateEffectTypeId(new ScriptEffect(), EffectTypeId.Script);
            ValidateEffectTypeId(new VideoCompositionEffect(), EffectTypeId.VideoComposition);
            ValidateEffectTypeId(new ImageCompositionEffect(), EffectTypeId.ImageComposition);
            ValidateEffectTypeId(new DeinterlacingEffect(), EffectTypeId.Deinterlacing);
            ValidateEffectTypeId(new CameraOptionEffect(), EffectTypeId.CameraOption);
            ValidateEffectTypeId(new OffScreenEffect(), EffectTypeId.OffScreen);
            ValidateEffectTypeId(new SplitEffect(), EffectTypeId.Split);
            ValidateEffectTypeId(new PartialFilterEffect(), EffectTypeId.PartialFilter);
            ValidateEffectTypeId(new AudioFadeEffect(), EffectTypeId.AudioFade);
            ValidateEffectTypeId(new AudioDelayEffect(), EffectTypeId.AudioDelay);
            ValidateEffectTypeId(new AudioDelayFilterEffect(), EffectTypeId.AudioDelayFilter);
            ValidateEffectTypeId(new MonauralEffect(), EffectTypeId.Monaural);
            ValidateEffectTypeId(new TimeControlEffect(), EffectTypeId.TimeControl);
            ValidateEffectTypeId(new GroupControlEffect(), EffectTypeId.GroupControl);
            ValidateEffectTypeId(new CameraControlEffect(), EffectTypeId.CameraControl);

            ValidateEffectTypeId(new CameraEffect(), EffectTypeId.CameraEffect);
            ValidateEffectTypeId(new CameraShadowEffect(), EffectTypeId.CameraShadow);
            ValidateEffectTypeId(new CameraScriptEffect(), EffectTypeId.CameraScript);
            ValidateEffectTypeId(new DenoiseFilterEffect(), EffectTypeId.DenoiseFilter);
            ValidateEffectTypeId(new SharpenFilterEffect(), EffectTypeId.SharpenFilter);
            ValidateEffectTypeId(new BlurFilter2Effect(), EffectTypeId.BlurFilter2);
            ValidateEffectTypeId(new ClipResizeFilterEffect(), EffectTypeId.ClipResizeFilter);
            ValidateEffectTypeId(new FillBorderEffect(), EffectTypeId.FillBorder);
            ValidateEffectTypeId(new ColorCorrection2Effect(), EffectTypeId.ColorCorrection2);
            ValidateEffectTypeId(new ColorCorrectionExEffect(), EffectTypeId.ColorCorrectionEx);
            ValidateEffectTypeId(new VolumeFilterEffect(), EffectTypeId.VolumeFilter);
        }
    }
}

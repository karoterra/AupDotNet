using Karoterra.AupDotNet.ExEdit.Effects;

namespace Karoterra.AupDotNet.ExEdit
{
    /// <summary>
    /// 標準のフィルタ効果ファクトリ。
    /// 組込みフィルタ効果はそれぞれの <see cref="Effect"/> 派生クラスのインスタンスを、
    /// 未知のフィルタ効果は <see cref="CustomEffect"/> のインスタンスを生成します。
    /// </summary>
    public class EffectFactory : IEffectFactory
    {
        /// <inheritdoc/>
        public Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
        {
            if (type.Id < 0 || EffectType.Defaults.Length <= type.Id)
            {
                return new CustomEffect(type, trackbars, checkboxes, data);
            }
            if (!type.Equals(EffectType.Defaults[type.Id]))
            {
                return new CustomEffect(type, trackbars, checkboxes, data);
            }

            return (EffectTypeId)type.Id switch
            {
                EffectTypeId.VideoFile => new VideoFileEffect(trackbars, checkboxes, data),
                EffectTypeId.ImageFile => new ImageFileEffect(trackbars, checkboxes, data),
                EffectTypeId.AudioFile => new AudioFileEffect(trackbars, checkboxes, data),
                EffectTypeId.Text => new TextEffect(trackbars, checkboxes, data),
                EffectTypeId.Figure => new FigureEffect(trackbars, checkboxes, data),
                EffectTypeId.FrameBuffer => new FrameBufferEffect(trackbars, checkboxes),
                EffectTypeId.Waveform => new WaveformEffect(trackbars, checkboxes, data),
                EffectTypeId.Scene => new SceneEffect(trackbars, checkboxes, data),
                EffectTypeId.SceneAudio => new SceneAudioEffect(trackbars, checkboxes, data),
                EffectTypeId.PreviousObject => new PreviousObjectEffect(trackbars, checkboxes),
                EffectTypeId.StandardDraw => new StandardDrawEffect(trackbars, checkboxes, data),
                EffectTypeId.ExtendedDraw => new ExtendedDrawEffect(trackbars, checkboxes, data),
                EffectTypeId.StandardPlayback => new StandardPlaybackEffect(trackbars, checkboxes),
                EffectTypeId.Particle => new ParticleEffect(trackbars, checkboxes, data),
                EffectTypeId.SceneChange => new SceneChangeEffect(trackbars, checkboxes, data),
                EffectTypeId.ColorCorrection => new ColorCorrectionEffect(trackbars, checkboxes),
                EffectTypeId.ColorCorrectionFilter => new ColorCorrectionFilterEffect(trackbars, checkboxes),
                EffectTypeId.Clipping => new ClippingEffect(trackbars, checkboxes),
                EffectTypeId.Blur => new BlurEffect(trackbars, checkboxes),
                EffectTypeId.BorderBlur => new BorderBlurEffect(trackbars, checkboxes),
                EffectTypeId.BlurFilter => new BlurFilterEffect(trackbars, checkboxes),
                EffectTypeId.Mosaic => new MosaicEffect(trackbars, checkboxes),
                EffectTypeId.MosaicFilter => new MosaicFilterEffect(trackbars, checkboxes),
                EffectTypeId.Emission => new EmissionEffect(trackbars, checkboxes, data),
                EffectTypeId.EmissionFilter => new EmissionFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.Flash => new FlashEffect(trackbars, checkboxes, data),
                EffectTypeId.Diffuse => new DiffuseEffect(trackbars, checkboxes),
                EffectTypeId.DiffuseFilter => new DiffuseFilterEffect(trackbars, checkboxes),
                EffectTypeId.Glow => new GlowEffect(trackbars, checkboxes, data),
                EffectTypeId.GlowFilter => new GlowFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.ChromaKey => new ChromaKeyEffect(trackbars, checkboxes, data),
                EffectTypeId.ColorKey => new ColorKeyEffect(trackbars, checkboxes, data),
                EffectTypeId.LuminanceKey => new LuminanceKeyEffect(trackbars, checkboxes, data),
                EffectTypeId.Light => new LightEffect(trackbars, checkboxes, data),
                EffectTypeId.Shadow => new ShadowEffect(trackbars, checkboxes, data),
                EffectTypeId.Border => new BorderEffect(trackbars, checkboxes, data),
                EffectTypeId.Bevel => new BevelEffect(trackbars, checkboxes),
                EffectTypeId.EdgeExtraction => new EdgeExtractionEffect(trackbars, checkboxes, data),
                EffectTypeId.Sharpen => new SharpenEffect(trackbars, checkboxes),
                EffectTypeId.Fade => new FadeEffect(trackbars, checkboxes),
                EffectTypeId.Wipe => new WipeEffect(trackbars, checkboxes, data),
                EffectTypeId.Mask => new MaskEffect(trackbars, checkboxes, data),
                EffectTypeId.DiagonalClipping => new DiagonalClippingEffect(trackbars, checkboxes),
                EffectTypeId.RadialBlur => new RadialBlurEffect(trackbars, checkboxes),
                EffectTypeId.RadialBlurFilter => new RadialBlurFilterEffect(trackbars, checkboxes),
                EffectTypeId.DirectionalBlur => new DirectionalBlurEffect(trackbars, checkboxes),
                EffectTypeId.DirectionalBlurFilter => new DirectionalBlurFilterEffect(trackbars, checkboxes),
                EffectTypeId.LensBlur => new LensBlurEffect(trackbars, checkboxes),
                EffectTypeId.LensBlurFilter => new LensBlurFilterEffect(trackbars, checkboxes),
                EffectTypeId.MotionBlur => new MotionBlurEffect(trackbars, checkboxes),
                EffectTypeId.MotionBlurFilter => new MotionBlurFilterEffect(trackbars, checkboxes),
                EffectTypeId.Position => new PositionEffect(trackbars, checkboxes),
                EffectTypeId.Zoom => new ZoomEffect(trackbars, checkboxes),
                EffectTypeId.Transparency => new TransparencyEffect(trackbars, checkboxes),
                EffectTypeId.Rotation => new RotationEffect(trackbars, checkboxes),
                EffectTypeId.AreaExpansion => new AreaExpansionEffect(trackbars, checkboxes),
                EffectTypeId.Resize => new ResizeEffect(trackbars, checkboxes),
                EffectTypeId.Rotation90 => new Rotation90Effect(trackbars, checkboxes),
                EffectTypeId.Vibration => new VibrationEffect(trackbars, checkboxes),
                EffectTypeId.VibrationFilter => new VibrationFilterEffect(trackbars, checkboxes),
                EffectTypeId.Inversion => new InversionEffect(trackbars, checkboxes),
                EffectTypeId.InversionFilter => new InversionFilterEffect(trackbars, checkboxes),
                EffectTypeId.Mirror => new MirrorEffect(trackbars, checkboxes, data),
                EffectTypeId.Raster => new RasterEffect(trackbars, checkboxes),
                EffectTypeId.RasterFilter => new RasterFilterEffect(trackbars, checkboxes),
                EffectTypeId.Ripple => new RippleEffect(trackbars, checkboxes, data),
                EffectTypeId.ImageLoop => new ImageLoopEffect(trackbars, checkboxes),
                EffectTypeId.ImageLoopFilter => new ImageLoopFilterEffect(trackbars, checkboxes),
                EffectTypeId.PolarTransform => new PolarTransformEffect(trackbars, checkboxes),
                EffectTypeId.Displacement => new DisplacementEffect(trackbars, checkboxes, data),
                EffectTypeId.Noise => new NoiseEffect(trackbars, checkboxes, data),
                EffectTypeId.ColorShift => new ColorShiftEffect(trackbars, checkboxes, data),
                EffectTypeId.ColorShiftFilter => new ColorShiftFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.Monochromatic => new MonochromaticEffect(trackbars, checkboxes, data),
                EffectTypeId.MonochromaticFilter => new MonochromaticFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.Gradation => new GradationEffect(trackbars, checkboxes, data),
                EffectTypeId.ColorSettingEx => new ColorSettingExEffect(trackbars, checkboxes),
                EffectTypeId.ColorSettingExFilter => new ColorSettingExFilterEffect(trackbars, checkboxes),
                EffectTypeId.GamutConversion => new GamutConversionEffect(trackbars, checkboxes, data),
                EffectTypeId.AnimationEffect => new AnimationEffect(trackbars, checkboxes, data),
                EffectTypeId.CustomObject => new CustomObjectEffect(trackbars, checkboxes, data),
                EffectTypeId.Script => new ScriptEffect(trackbars, checkboxes, data),
                EffectTypeId.VideoComposition => new VideoCompositionEffect(trackbars, checkboxes, data),
                EffectTypeId.ImageComposition => new ImageCompositionEffect(trackbars, checkboxes, data),
                EffectTypeId.Deinterlacing => new DeinterlacingEffect(trackbars, checkboxes, data),
                EffectTypeId.CameraOption => new CameraOptionEffect(trackbars, checkboxes),
                EffectTypeId.OffScreen => new OffScreenEffect(trackbars, checkboxes),
                EffectTypeId.Split => new SplitEffect(trackbars, checkboxes),
                EffectTypeId.PartialFilter => new PartialFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.AudioFade => new AudioFadeEffect(trackbars, checkboxes),
                EffectTypeId.AudioDelay => new AudioDelayEffect(trackbars, checkboxes),
                EffectTypeId.AudioDelayFilter => new AudioDelayFilterEffect(trackbars, checkboxes),
                EffectTypeId.Monaural => new MonauralEffect(trackbars, checkboxes),
                EffectTypeId.TimeControl => new TimeControlEffect(trackbars, checkboxes, data),
                EffectTypeId.GroupControl => new GroupControlEffect(trackbars, checkboxes, data),
                EffectTypeId.CameraControl => new CameraControlEffect(trackbars, checkboxes, data),
                EffectTypeId.CameraControlEx => new CustomEffect(type, trackbars, checkboxes, data),
                EffectTypeId.CameraEffect => new CameraEffect(trackbars, checkboxes, data),
                EffectTypeId.CameraShadow => new CameraShadowEffect(trackbars, checkboxes),
                EffectTypeId.CameraScript => new CameraScriptEffect(trackbars, checkboxes, data),
                EffectTypeId.DenoiseFilter => new DenoiseFilterEffect(trackbars, checkboxes),
                EffectTypeId.SharpenFilter => new SharpenFilterEffect(trackbars, checkboxes),
                EffectTypeId.BlurFilter2 => new BlurFilter2Effect(trackbars, checkboxes),
                EffectTypeId.ClipResizeFilter => new ClipResizeFilterEffect(trackbars, checkboxes, data),
                EffectTypeId.FillBorder => new FillBorderEffect(trackbars, checkboxes),
                EffectTypeId.ColorCorrection2 => new ColorCorrection2Effect(trackbars, checkboxes),
                EffectTypeId.ColorCorrectionEx => new ColorCorrectionExEffect(trackbars, checkboxes),
                EffectTypeId.VolumeFilter => new VolumeFilterEffect(trackbars, checkboxes),
                _ => new CustomEffect(type, trackbars, checkboxes, data),
            };
        }
    }
}

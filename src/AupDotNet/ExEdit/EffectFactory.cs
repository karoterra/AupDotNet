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

            switch ((EffectTypeId)type.Id)
            {
                case EffectTypeId.VideoFile: return new VideoFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageFile: return new ImageFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.AudioFile: return new AudioFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.Text: return new TextEffect(trackbars, checkboxes, data);
                case EffectTypeId.Figure: return new FigureEffect(trackbars, checkboxes, data);
                case EffectTypeId.FrameBuffer: return new FrameBufferEffect(trackbars, checkboxes);
                case EffectTypeId.Waveform: return new WaveformEffect(trackbars, checkboxes, data);
                case EffectTypeId.Scene: return new SceneEffect(trackbars, checkboxes, data);
                case EffectTypeId.SceneAudio: return new SceneAudioEffect(trackbars, checkboxes, data);
                case EffectTypeId.PreviousObject: return new PreviousObjectEffect(trackbars, checkboxes);
                case EffectTypeId.StandardDraw: return new StandardDrawEffect(trackbars, checkboxes, data);
                case EffectTypeId.ExtendedDraw: return new ExtendedDrawEffect(trackbars, checkboxes, data);
                case EffectTypeId.StandardPlayback: return new StandardPlaybackEffect(trackbars, checkboxes);
                case EffectTypeId.Particle: return new ParticleEffect(trackbars, checkboxes, data);
                case EffectTypeId.SceneChange: return new SceneChangeEffect(trackbars, checkboxes, data);
                case EffectTypeId.ColorCorrection: return new ColorCorrectionEffect(trackbars, checkboxes);
                case EffectTypeId.ColorCorrectionFilter: return new ColorCorrectionFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Clipping: return new ClippingEffect(trackbars, checkboxes);
                case EffectTypeId.Blur: return new BlurEffect(trackbars, checkboxes);
                case EffectTypeId.BorderBlur: return new BorderBlurEffect(trackbars, checkboxes);
                case EffectTypeId.BlurFilter: return new BlurFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Mosaic: return new MosaicEffect(trackbars, checkboxes);
                case EffectTypeId.MosaicFilter: return new MosaicFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Emission: return new EmissionEffect(trackbars, checkboxes, data);
                case EffectTypeId.EmissionFilter: return new EmissionFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.Flash: return new FlashEffect(trackbars, checkboxes, data);
                case EffectTypeId.Diffuse: return new DiffuseEffect(trackbars, checkboxes);
                case EffectTypeId.DiffuseFilter: return new DiffuseFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Glow: return new GlowEffect(trackbars, checkboxes, data);
                case EffectTypeId.GlowFilter: return new GlowFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.ChromaKey: return new ChromaKeyEffect(trackbars, checkboxes, data);
                case EffectTypeId.ColorKey: return new ColorKeyEffect(trackbars, checkboxes, data);
                case EffectTypeId.LuminanceKey: return new LuminanceKeyEffect(trackbars, checkboxes, data);
                case EffectTypeId.Light: return new LightEffect(trackbars, checkboxes, data);
                case EffectTypeId.Shadow: return new ShadowEffect(trackbars, checkboxes, data);
                case EffectTypeId.Border: return new BorderEffect(trackbars, checkboxes, data);
                case EffectTypeId.Bevel: return new BevelEffect(trackbars, checkboxes);
                case EffectTypeId.EdgeExtraction: return new EdgeExtractionEffect(trackbars, checkboxes, data);
                case EffectTypeId.Sharpen: return new SharpenEffect(trackbars, checkboxes);
                case EffectTypeId.Fade: return new FadeEffect(trackbars, checkboxes);
                case EffectTypeId.Wipe: return new WipeEffect(trackbars, checkboxes, data);
                case EffectTypeId.Mask: return new MaskEffect(trackbars, checkboxes, data);
                case EffectTypeId.DiagonalClipping: return new DiagonalClippingEffect(trackbars, checkboxes);
                case EffectTypeId.RadialBlur: return new RadialBlurEffect(trackbars, checkboxes);
                case EffectTypeId.RadialBlurFilter: return new RadialBlurFilterEffect(trackbars, checkboxes);
                case EffectTypeId.DirectionalBlur: return new DirectionalBlurEffect(trackbars, checkboxes);
                case EffectTypeId.DirectionalBlurFilter: return new DirectionalBlurFilterEffect(trackbars, checkboxes);
                case EffectTypeId.LensBlur: return new LensBlurEffect(trackbars, checkboxes);
                case EffectTypeId.LensBlurFilter: return new LensBlurFilterEffect(trackbars, checkboxes);
                case EffectTypeId.MotionBlur: return new MotionBlurEffect(trackbars, checkboxes);
                case EffectTypeId.MotionBlurFilter: return new MotionBlurFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Position: return new PositionEffect(trackbars, checkboxes);
                case EffectTypeId.Zoom: return new ZoomEffect(trackbars, checkboxes);
                case EffectTypeId.Transparency: return new TransparencyEffect(trackbars, checkboxes);
                case EffectTypeId.Rotation: return new RotationEffect(trackbars, checkboxes);
                case EffectTypeId.AreaExpansion: return new AreaExpansionEffect(trackbars, checkboxes);
                case EffectTypeId.Resize: return new ResizeEffect(trackbars, checkboxes);
                case EffectTypeId.Rotation90: return new Rotation90Effect(trackbars, checkboxes);
                case EffectTypeId.Vibration: return new VibrationEffect(trackbars, checkboxes);
                case EffectTypeId.VibrationFilter: return new VibrationFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Inversion: return new InversionEffect(trackbars, checkboxes);
                case EffectTypeId.InversionFilter: return new InversionFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Mirror: return new MirrorEffect(trackbars, checkboxes, data);
                case EffectTypeId.Raster: return new RasterEffect(trackbars, checkboxes);
                case EffectTypeId.RasterFilter: return new RasterFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Ripple: return new RippleEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageLoop: return new ImageLoopEffect(trackbars, checkboxes);
                case EffectTypeId.ImageLoopFilter: return new ImageLoopFilterEffect(trackbars, checkboxes);
                case EffectTypeId.PolarTransform: return new PolarTransformEffect(trackbars, checkboxes);
                case EffectTypeId.Displacement: return new DisplacementEffect(trackbars, checkboxes, data);
                case EffectTypeId.Noise: return new NoiseEffect(trackbars, checkboxes, data);
                case EffectTypeId.ColorShift: return new ColorShiftEffect(trackbars, checkboxes, data);
                case EffectTypeId.ColorShiftFilter: return new ColorShiftFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.Monochromatic: return new MonochromaticEffect(trackbars, checkboxes, data);
                case EffectTypeId.MonochromaticFilter: return new MonochromaticFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.Gradation: return new GradationEffect(trackbars, checkboxes, data);
                case EffectTypeId.ColorSettingEx: return new ColorSettingExEffect(trackbars, checkboxes);
                case EffectTypeId.ColorSettingExFilter: return new ColorSettingExFilterEffect(trackbars, checkboxes);
                case EffectTypeId.GamutConversion: return new GamutConversionEffect(trackbars, checkboxes, data);
                case EffectTypeId.AnimationEffect: return new AnimationEffect(trackbars, checkboxes, data);
                case EffectTypeId.CustomObject: return new CustomObjectEffect(trackbars, checkboxes, data);
                case EffectTypeId.Script: return new ScriptEffect(trackbars, checkboxes, data);
                case EffectTypeId.VideoComposition: return new VideoCompositionEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageComposition: return new ImageCompositionEffect(trackbars, checkboxes, data);
                case EffectTypeId.Deinterlacing: return new DeinterlacingEffect(trackbars, checkboxes, data);
                case EffectTypeId.CameraOption: return new CameraOptionEffect(trackbars, checkboxes);
                case EffectTypeId.OffScreen: return new OffScreenEffect(trackbars, checkboxes);
                case EffectTypeId.Split: return new SplitEffect(trackbars, checkboxes);
                case EffectTypeId.PartialFilter: return new PartialFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.AudioFade: return new AudioFadeEffect(trackbars, checkboxes);
                case EffectTypeId.AudioDelay: return new AudioDelayEffect(trackbars, checkboxes);
                case EffectTypeId.AudioDelayFilter: return new AudioDelayFilterEffect(trackbars, checkboxes);
                case EffectTypeId.Monaural: return new MonauralEffect(trackbars, checkboxes);
                case EffectTypeId.TimeControl: return new TimeControlEffect(trackbars, checkboxes, data);
                case EffectTypeId.GroupControl: return new GroupControlEffect(trackbars, checkboxes, data);
                case EffectTypeId.CameraControl: return new CameraControlEffect(trackbars, checkboxes, data);

                case EffectTypeId.CameraEffect: return new CameraEffect(trackbars, checkboxes, data);
                case EffectTypeId.CameraShadow: return new CameraShadowEffect(trackbars, checkboxes);
                case EffectTypeId.CameraScript: return new CameraScriptEffect(trackbars, checkboxes, data);
                case EffectTypeId.DenoiseFilter: return new DenoiseFilterEffect(trackbars, checkboxes);
                case EffectTypeId.SharpenFilter: return new SharpenFilterEffect(trackbars, checkboxes);
                case EffectTypeId.BlurFilter2: return new BlurFilter2Effect(trackbars, checkboxes);
                case EffectTypeId.ClipResizeFilter: return new ClipResizeFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.FillBorder: return new FillBorderEffect(trackbars, checkboxes);
                case EffectTypeId.ColorCorrection2: return new ColorCorrection2Effect(trackbars, checkboxes);
                case EffectTypeId.ColorCorrectionEx: return new ColorCorrectionExEffect(trackbars, checkboxes);
                case EffectTypeId.VolumeFilter: return new VolumeFilterEffect(trackbars, checkboxes);

                default: return new CustomEffect(type, trackbars, checkboxes, data);
            }
        }
    }
}

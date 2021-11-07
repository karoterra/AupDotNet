using System;
using Karoterra.AupDotNet.ExEdit.Effects;

namespace Karoterra.AupDotNet.ExEdit
{
    public static class EffectFactory
    {
        public static Effect Create(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
        {
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


                case EffectTypeId.Shadow: return new ShadowEffect(trackbars, checkboxes, data);
                case EffectTypeId.Border: return new BorderEffect(trackbars, checkboxes, data);

                case EffectTypeId.Wipe: return new WipeEffect(trackbars, checkboxes, data);
                case EffectTypeId.Mask: return new MaskEffect(trackbars, checkboxes, data);

                case EffectTypeId.Displacement: return new DisplacementEffect(trackbars, checkboxes, data);

                case EffectTypeId.AnimationEffect: return new AnimationEffect(trackbars, checkboxes, data);
                case EffectTypeId.CustomObject: return new CustomObjectEffect(trackbars, checkboxes, data);

                case EffectTypeId.VideoComposition: return new VideoCompositionEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageComposition: return new ImageCompositionEffect(trackbars, checkboxes, data);

                case EffectTypeId.PartialFilter: return new PartialFilterEffect(trackbars, checkboxes, data);

                case EffectTypeId.CameraEffect: return new CameraEffect(trackbars, checkboxes, data);

                default: return new CustomEffect(type, trackbars, checkboxes, data);
            }
        }
    }
}

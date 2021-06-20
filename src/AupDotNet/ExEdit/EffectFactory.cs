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
                case EffectTypeId.VideoFile:
                    return new VideoFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageFile:
                    return new ImageFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.AudioFile:
                    return new AudioFileEffect(trackbars, checkboxes, data);
                case EffectTypeId.Text:
                    return new TextEffect(trackbars, checkboxes, data);
                case EffectTypeId.Figure:
                    return new FigureEffect(trackbars, checkboxes, data);
                case EffectTypeId.FrameBuffer:
                    return new FrameBufferEffect(trackbars, checkboxes);
                case EffectTypeId.Waveform:
                    return new WaveformEffect(trackbars, checkboxes, data);
                case EffectTypeId.StandardDraw:
                    return new StandardDrawEffect(trackbars, checkboxes, data);
                case EffectTypeId.ExtendedDraw:
                    return new ExtendedDrawEffect(trackbars, checkboxes, data);
                case EffectTypeId.StandardPlayback:
                    return new StandardPlaybackEffect(trackbars, checkboxes);
                case EffectTypeId.SceneChange:
                    return new SceneChangeEffect(trackbars, checkboxes, data);
                case EffectTypeId.Shadow:
                    return new ShadowEffect(trackbars, checkboxes, data);
                case EffectTypeId.Border:
                    return new BorderEffect(trackbars, checkboxes, data);
                case EffectTypeId.Wipe:
                    return new WipeEffect(trackbars, checkboxes, data);
                case EffectTypeId.Mask:
                    return new MaskEffect(trackbars, checkboxes, data);
                case EffectTypeId.Displacement:
                    return new DisplacementEffect(trackbars, checkboxes, data);
                case EffectTypeId.AnimationEffect:
                    return new AnimationEffect(trackbars, checkboxes, data);
                case EffectTypeId.CustomObject:
                    return new CustomObjectEffect(trackbars, checkboxes, data);
                case EffectTypeId.VideoComposition:
                    return new VideoCompositionEffect(trackbars, checkboxes, data);
                case EffectTypeId.ImageComposition:
                    return new ImageCompositionEffect(trackbars, checkboxes, data);
                case EffectTypeId.PartialFilter:
                    return new PartialFilterEffect(trackbars, checkboxes, data);
                case EffectTypeId.CameraEffect:
                    return new CameraEffect(trackbars, checkboxes, data);
                default:
                    return new CustomEffect(type, trackbars, checkboxes, data);
            }
        }
    }
}

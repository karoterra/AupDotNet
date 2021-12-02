using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シーン(音声)
    /// </summary>
    public class SceneAudioEffect : Effect
    {
        public static EffectType EffectType { get; }

        /// <summary>再生位置</summary>
        public Trackbar Position => Trackbars[0];

        /// <summary>再生速度</summary>
        public Trackbar Speed => Trackbars[1];

        /// <summary>ループ再生</summary>
        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        /// <summary>シーンと連携</summary>
        public bool Link
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        /// <summary>シーン選択</summary>
        public int Scene { get; set; }

        public SceneAudioEffect()
            : base(EffectType)
        {
        }

        public SceneAudioEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public SceneAudioEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    Scene = data.ToInt32();
                }
                else if (data.Length != 0)
                {
                    throw new ArgumentException("data's length is invalid.");
                }
            }
        }

        public override byte[] DumpExtData()
        {
            var data = new byte[Type.ExtSize];
            Scene.ToBytes().CopyTo(data, 0);
            return data;
        }

        static SceneAudioEffect()
        {
            EffectType = new EffectType(
                8, 0x04200408, 2, 3, 4, "シーン(音声)",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("再生位置", 1, 1, 1, 1),
                    new TrackbarDefinition("再生速度", 10, 100, 20000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ再生", true, 0),
                    new CheckboxDefinition("シーンと連携", true, 1),
                    new CheckboxDefinition("シーン選択", false, 0),
                }
            );
        }
    }
}

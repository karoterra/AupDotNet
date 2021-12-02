using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シーン
    /// </summary>
    public class SceneEffect : Effect
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

        /// <summary>シーン選択</summary>
        public int Scene { get; set; }

        public SceneEffect()
            : base(EffectType)
        {
        }

        public SceneEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType, trackbars, checkboxes)
        {
        }

        public SceneEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
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

        static SceneEffect()
        {
            EffectType = new EffectType(
                7, 0x04000408, 2, 2, 4, "シーン",
                new TrackbarDefinition[]
                {
                    new TrackbarDefinition("再生位置", 1, 1, 1, 1),
                    new TrackbarDefinition("再生速度", 10, -20000, 20000, 1000),
                },
                new CheckboxDefinition[]
                {
                    new CheckboxDefinition("ループ再生", true, 0),
                    new CheckboxDefinition("シーン選択", false, 0),
                }
            );
        }
    }
}

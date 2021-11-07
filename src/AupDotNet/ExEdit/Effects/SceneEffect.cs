using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    /// <summary>
    /// シーン
    /// </summary>
    public class SceneEffect : Effect
    {
        private const int Id = (int)EffectTypeId.Scene;

        public Trackbar Position => Trackbars[0];
        public Trackbar Speed => Trackbars[1];

        public bool Loop
        {
            get => Checkboxes[0] != 0;
            set => Checkboxes[0] = value ? 1 : 0;
        }

        public int Scene { get; set; }

        public SceneEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public SceneEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public SceneEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
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
    }
}

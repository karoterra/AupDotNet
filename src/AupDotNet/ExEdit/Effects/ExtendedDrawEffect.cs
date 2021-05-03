using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public class ExtendedDrawEffect : Effect
    {
        private const int Id = (int)EffectTypeId.ExtendedDraw;

        public Trackbar X => Trackbars[0];
        public Trackbar Y => Trackbars[1];
        public Trackbar Z => Trackbars[2];
        public Trackbar Zoom => Trackbars[3];
        public Trackbar Alpha => Trackbars[4];
        public Trackbar AspectRatio => Trackbars[5];
        public Trackbar RotateX => Trackbars[6];
        public Trackbar RotateY => Trackbars[7];
        public Trackbar RotateZ => Trackbars[8];
        public Trackbar CenterX => Trackbars[9];
        public Trackbar CenterY => Trackbars[10];
        public Trackbar CenterZ => Trackbars[11];

        public bool BackfaceCulling
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public BlendMode BlendMode { get; set; }

        public ExtendedDrawEffect()
            : base(EffectType.Defaults[Id])
        {
        }

        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
        }

        public ExtendedDrawEffect(Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(EffectType.Defaults[Id], trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    BlendMode = (BlendMode)span.Slice(0, 4).ToInt32();
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
            ((int)BlendMode).ToBytes().CopyTo(data, 0);
            return data;
        }
    }
}

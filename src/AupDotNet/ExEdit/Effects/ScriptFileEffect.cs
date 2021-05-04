using System;
using Karoterra.AupDotNet.Extensions;

namespace Karoterra.AupDotNet.ExEdit.Effects
{
    public abstract class ScriptFileEffect : Effect
    {
        public readonly int MaxNameLength = 256;
        public readonly int MaxParamsLength = 256;

        public bool Check0
        {
            get => Checkboxes[1] != 0;
            set => Checkboxes[1] = value ? 1 : 0;
        }

        public int ScriptId { get; set; }
        public ScriptDirectory Directory { get; set; }

        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (value.GetSjisByteCount() >= MaxNameLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Name), MaxNameLength);
                }
                _name = value;
            }
        }

        private string _params = "";
        public string Params
        {
            get => _params;
            set
            {
                if (value.GetSjisByteCount() >= MaxParamsLength)
                {
                    throw new MaxByteCountOfStringException(nameof(Params), MaxParamsLength);
                }
                _params = value;
            }
        }

        public ScriptFileEffect(EffectType type)
            : base(type)
        {
        }

        public ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes)
            : base(type, trackbars, checkboxes)
        {
        }

        public ScriptFileEffect(EffectType type, Trackbar[] trackbars, int[] checkboxes, byte[] data)
            : base(type, trackbars, checkboxes)
        {
            if (data != null)
            {
                if (data.Length == Type.ExtSize)
                {
                    var span = new ReadOnlySpan<byte>(data);
                    ScriptId = span.Slice(0, 2).ToInt16();
                    Directory = (ScriptDirectory)span.Slice(2, 2).ToInt16();
                    Name = span.Slice(4, MaxNameLength).ToCleanSjisString();
                    Params = span.Slice(0x104, MaxParamsLength).ToCleanSjisString();
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
            ((short)ScriptId).ToBytes().CopyTo(data, 0);
            ((short)Directory).ToBytes().CopyTo(data, 2);
            Name.ToSjisBytes(MaxNameLength).CopyTo(data, 4);
            Params.ToSjisBytes(MaxParamsLength).CopyTo(data, 0x104);
            return data;
        }
    }
}

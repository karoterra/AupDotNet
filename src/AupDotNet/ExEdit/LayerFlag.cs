using System;

namespace Karoterra.AupDotNet.ExEdit
{
    [Flags]
    public enum LayerFlag
    {
        Hide = 1,
        Lock = 2,
        Link = 0x10,
        Clipping = 0x20,
    }
}

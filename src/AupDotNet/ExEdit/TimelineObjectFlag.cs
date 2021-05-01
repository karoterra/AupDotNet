using System;

namespace Karoterra.AupDotNet.ExEdit
{
    [Flags]
    public enum TimelineObjectFlag
    {
        Enable = 1,

        Clipping = 0x0000_0100,
        Camera = 0x0000_0200,

        Media = 0x0001_0000,
        Audio = 0x0002_0000,
        MediaFilter = 0x0004_0000,
        Control = 0x0008_0000,
        Range = 0x0010_0000,
    }
}

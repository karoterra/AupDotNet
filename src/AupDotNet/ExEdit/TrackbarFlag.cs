using System;

namespace Karoterra.AupDotNet.ExEdit
{
    [Flags]
    public enum TrackbarFlag
    {
        Stop = 0,
        Liner = 1,
        Curve = 2,
        Step = 3,
        IgnoreKeyframe = 4,
        idouryoushitei = 5,
        Random = 6,
        AccelDecel = 7,
        Repeat = 8,
        Script = 0xF,

        Acceleration = 0x40,
        Deceleration = 0x20,
    }
}

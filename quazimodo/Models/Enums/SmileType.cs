using System;

namespace quazimodo.Models.Enums
{
    [Flags]
    public enum SmileType
    {
        NotSet = 0,
        Positive = 1,
        Negative = 2,
        Neutral = 4,
        Record = 8
    }
}
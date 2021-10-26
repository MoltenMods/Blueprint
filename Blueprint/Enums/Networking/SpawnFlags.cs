using System;

namespace Blueprint.Enums.Networking
{
    [Flags]
    public enum SpawnFlags : byte
    {
        None = 0,
        IsClientCharacter = 1
    }
}
using System;

namespace Blueprint.Enums.Networking
{
    [Flags]
    public enum PlayerStates
    {
        Disconnected = 1,
        Impostor = 2,
        Dead = 4
    }
}
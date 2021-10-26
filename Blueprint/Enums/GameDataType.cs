namespace Blueprint.Enums
{
    public enum GameDataType : byte
    {
        Data = 0,
        Rpc = 1,
        Spawn = 4,
        Despawn = 5,
        SceneChange = 6,
        Ready = 7,
        ChangeSettings = 8,
        ConsoleDeclareClientPlatform = 205,
        Ps4RoomRequest = 206
    }
}
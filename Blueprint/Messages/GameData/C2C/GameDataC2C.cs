using Blueprint.Messages.Objects;
using Singularity.Hazel;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.C2C
{
    public static class GameDataC2C
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode, GameData gameData)
        {
            writer.StartMessage((byte) MessageType.GameData);
            
            writer.Write(gameCode.Value);
            
            // TODO: not sure if this is correct
            using var dataWriter = MessageWriter.Get();
            gameData.Serialize(dataWriter);
            writer.Write(dataWriter.ToByteArray(false));
        }
    }
}
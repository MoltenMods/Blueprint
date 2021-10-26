using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2H
{
    public static class WaitForHostC2H
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode, uint playerId)
        {
            writer.StartMessage((byte) MessageType.WaitForHost);
            
            writer.Write(gameCode.Value);
            writer.Write(playerId);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out GameCode gameCode, out uint playerId)
        {
            gameCode = GameCode.CreateFrom(reader);
            playerId = reader.ReadUInt32();
        }
    }
}
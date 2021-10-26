using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class JoinGameS2C
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode, uint joiningPlayerId, uint hostId)
        {
            writer.StartMessage((byte) MessageType.JoinGame);
            
            writer.Write(gameCode.Value);
            writer.Write(joiningPlayerId);
            writer.Write(hostId);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint joiningPlayerId,
            out uint hostId)
        {
            gameCode = GameCode.CreateFrom(reader);
            joiningPlayerId = reader.ReadUInt32();
            hostId = reader.ReadUInt32();
        }
    }
}
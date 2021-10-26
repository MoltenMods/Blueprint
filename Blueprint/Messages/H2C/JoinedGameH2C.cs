using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.H2C
{
    public static class JoinedGameH2C
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            uint playerId,
            uint hostId,
            uint[] otherPlayerIds)
        {
            writer.StartMessage((byte) MessageType.JoinedGame);
            
            writer.Write(gameCode.Value);
            writer.WritePacked(playerId);
            writer.WritePacked(hostId);
            
            writer.WritePacked((uint) otherPlayerIds.Length);
            foreach (var otherPlayerId in otherPlayerIds)
            {
                writer.WritePacked(otherPlayerId);
            }
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint playerId,
            out uint hostId,
            out uint[] otherPlayerIds)
        {
            gameCode = GameCode.CreateFrom(reader);
            playerId = reader.ReadPackedUInt32();
            hostId = reader.ReadPackedUInt32();

            var length = reader.ReadPackedUInt32();
            otherPlayerIds = new uint[length];

            for (var i = 0; i < length; i++)
            {
                otherPlayerIds[i] = reader.ReadPackedUInt32();
            }
        }
    }
}
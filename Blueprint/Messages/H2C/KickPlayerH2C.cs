using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.H2C
{
    public static class KickPlayerH2C
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            uint playerId,
            bool isBanned,
            DisconnectReason? disconnectReason = null)
        {
            writer.StartMessage((byte) MessageType.KickPlayer);
            
            writer.Write(gameCode.Value);
            writer.WritePacked(playerId);
            writer.Write(isBanned);

            if (disconnectReason != null)
            {
                writer.Write((byte) disconnectReason);
            }
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint playerId,
            out bool isBanned,
            out DisconnectReason? disconnectReason)
        {
            gameCode = GameCode.CreateFrom(reader);
            playerId = reader.ReadPackedUInt32();
            isBanned = reader.ReadBoolean();

            disconnectReason = reader.Position < reader.Length ? (DisconnectReason) reader.ReadByte() : null;
        }
    }
}
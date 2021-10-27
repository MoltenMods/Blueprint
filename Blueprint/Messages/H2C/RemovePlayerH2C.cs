using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.H2C
{
    public static class RemovePlayer
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            uint playerId,
            DisconnectReason disconnectReason)
        {
            writer.StartMessage((byte) MessageType.RemovePlayer);
            
            writer.Write(gameCode.Value);
            writer.WritePacked(playerId);
            writer.Write((byte) disconnectReason);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint playerId,
            out DisconnectReason disconnectReason)
        {
            gameCode = GameCode.CreateFrom(reader);
            playerId = reader.ReadPackedUInt32();
            disconnectReason = (DisconnectReason) reader.ReadByte();
        }
    }
}
using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class RemovePlayerS2C
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            uint clientId,
            uint hostId,
            DisconnectReason disconnectReason)
        {
            writer.StartMessage((byte) MessageType.RemovePlayer);
            
            writer.Write(gameCode.Value);
            writer.WritePacked(clientId);
            writer.WritePacked(hostId);
            writer.Write((byte) disconnectReason);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint clientId,
            out uint hostId,
            out DisconnectReason disconnectReason)
        {
            gameCode = GameCode.CreateFrom(reader);
            clientId = reader.ReadPackedUInt32();
            hostId = reader.ReadPackedUInt32();
            disconnectReason = (DisconnectReason) reader.ReadByte();
        }
    }
}
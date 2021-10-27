using Blueprint.Enums.Networking;
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

        public static bool TryDeserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint? joiningPlayerId,
            out uint? hostId)
        {
            var originalPosition = reader.Position;
            var value = reader.ReadInt32();

            if (reader.Position >= reader.Length)
            {
                gameCode = null;
                joiningPlayerId = null;
                hostId = null;
                
                reader.Seek(originalPosition);
                
                return false;
            }
            
            gameCode = GameCode.CreateFrom(value);
            joiningPlayerId = reader.ReadUInt32();
            hostId = reader.ReadUInt32();

            return true;
        }

        public static bool TryDeserialize(IMessageReader reader, out DisconnectReason? disconnectReason)
        {
            var originalPosition = reader.Position;
            var value = reader.ReadInt32();

            if (reader.Position < reader.Length)
            {
                disconnectReason = null;
                
                reader.Seek(originalPosition);

                return false;
            }

            disconnectReason = (DisconnectReason) value;

            return true;
        }
    }
}
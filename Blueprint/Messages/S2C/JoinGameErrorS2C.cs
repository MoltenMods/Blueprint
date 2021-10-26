using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class JoinGameErrorS2C
    {
        public static void Serialize(IMessageWriter writer, DisconnectReason disconnectReason)
        {
            writer.StartMessage((byte) MessageType.JoinGame);
            
            writer.Write((int) disconnectReason);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out DisconnectReason disconnectReason)
        {
            disconnectReason = (DisconnectReason) reader.ReadInt32();
        }
    }
}
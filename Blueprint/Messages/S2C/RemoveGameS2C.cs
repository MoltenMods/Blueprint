using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class RemoveGameS2C
    {
        public static void Serialize(IMessageWriter writer, DisconnectReason? disconnectReason = null)
        {
            writer.StartMessage((byte) MessageType.RemoveGame);

            if (disconnectReason != null)
            {
                writer.Write((byte) disconnectReason);
            }
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out DisconnectReason? disconnectReason)
        {
            disconnectReason = reader.Position < reader.Length ? (DisconnectReason) reader.ReadByte() : null;
        }
    }
}
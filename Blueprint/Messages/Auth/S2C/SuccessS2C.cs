using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.Auth.S2C
{
    public static class SuccessS2C
    {
        public static void Serialize(IMessageWriter writer, uint nonce)
        {
            writer.StartMessage((byte) AuthMessageType.Success);
            
            writer.Write(nonce);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out uint nonce)
        {
            nonce = reader.ReadUInt32();
        }
    }
}
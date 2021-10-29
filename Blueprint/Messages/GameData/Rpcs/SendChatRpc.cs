using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public static class SendChatRpc
    {
        public static void Serialize(IMessageWriter writer, string message)
        {
            writer.Write((byte) RpcType.SendChat);
            
            writer.Write(message);
        }

        public static void Deserialize(IMessageReader reader, out string message)
        {
            message = reader.ReadString();
        }
    }
}
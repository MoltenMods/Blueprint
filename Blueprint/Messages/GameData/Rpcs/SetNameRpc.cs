using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public static class SetNameRpc
    {
        public static void Serialize(IMessageWriter writer, string name)
        {
            writer.Write((byte) RpcType.SetName);
            
            writer.Write(name);
        }

        public static void Deserialize(IMessageReader reader, out string name)
        {
            name = reader.ReadString();
        }
    }
}
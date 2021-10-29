using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public static class CompleteTaskRpc
    {
        public static void Serialize(IMessageWriter writer, uint taskIndex)
        {
            writer.Write((byte) RpcType.CompleteTask);
            
            writer.WritePacked(taskIndex);
        }

        public static void Deserialize(IMessageReader reader, out uint taskIndex)
        {
            taskIndex = reader.ReadPackedUInt32();
        }
    }
}
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public static class PlayAnimationRpc
    {
        public static void Serialize(IMessageWriter writer, TaskType taskType)
        {
            writer.Write((byte) RpcType.PlayAnimation);
            
            writer.Write((byte) taskType);
        }

        public static void Deserialize(IMessageReader reader, out TaskType taskType)
        {
            taskType = (TaskType) reader.ReadByte();
        }
    }
}
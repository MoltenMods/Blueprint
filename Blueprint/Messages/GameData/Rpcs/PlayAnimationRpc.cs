using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public class PlayAnimationRpc : Rpc
    {
        public override RpcType RpcType => RpcType.PlayAnimation;
        
        public TaskType TaskType { get; set; }

        public PlayAnimationRpc(TaskType taskType)
        {
            this.TaskType = taskType;
        }

        protected override void Write(IMessageWriter writer)
        {
            writer.Write((byte) this.TaskType);
        }

        protected override void Read(IMessageReader reader)
        {
            this.TaskType = (TaskType) reader.ReadByte();
        }
    }
}
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public class CompleteTaskRpc : Rpc
    {
        public override RpcType RpcType => RpcType.CompleteTask;
        
        public uint TaskIndex { get; set; }

        public CompleteTaskRpc(uint taskIndex)
        {
            this.TaskIndex = taskIndex;
        }

        protected override void Write(IMessageWriter writer)
        {
            writer.WritePacked(this.TaskIndex);
        }

        protected override void Read(IMessageReader reader)
        {
            this.TaskIndex = reader.ReadPackedUInt32();
        }
    }
}
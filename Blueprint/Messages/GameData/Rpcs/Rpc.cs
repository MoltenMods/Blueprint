using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.Rpcs
{
    public abstract class Rpc
    {
        public abstract RpcType RpcType { get; }

        public void Serialize(IMessageWriter writer)
        {
            writer.StartMessage((byte) this.RpcType);
            
            this.Write(writer);
            
            writer.EndMessage();
        }

        public void Deserialize(IMessageReader reader)
        {
            this.Read(reader);
        }
        
        protected abstract void Write(IMessageWriter writer);

        protected abstract void Read(IMessageReader reader);
    }
}
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.SystemTypes
{
    public abstract class System
    {
        public abstract SystemType SystemType { get; }
        
        public void Serialize(IMessageWriter writer)
        {
            this.Write(writer);
        }

        public void Deserialize(IMessageReader reader)
        {
            this.Read(reader);
        }
        
        protected abstract void Write(IMessageWriter writer);

        protected abstract void Read(IMessageReader reader);
    }
}
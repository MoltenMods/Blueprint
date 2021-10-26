using Blueprint.Enums;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData
{
    public abstract class GameData
    {
        public abstract GameDataType GameDataType { get; }

        public void Serialize(IMessageWriter writer)
        {
            writer.StartMessage((byte) this.GameDataType);
            
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
using Blueprint.Enums;
using Blueprint.Messages.InnerNetObjects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.H2C
{
    public class DataH2C : GameData
    {
        public override GameDataType GameDataType => GameDataType.Data;
        
        public InnerNetObject InnerNetObject { get; set; }

        public DataH2C(InnerNetObject innerNetObject)
        {
            this.InnerNetObject = innerNetObject;
        }

        protected override void Write(IMessageWriter writer)
        {
            this.InnerNetObject.Serialize(writer, false);
        }

        protected override void Read(IMessageReader reader)
        {
            this.InnerNetObject.Deserialize(reader, false);
        }
    }
}
using System.Numerics;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class CustomNetworkTransform : InnerNetObject<CustomNetworkTransform>
    {
        public ushort LastSequenceId { get; set; }
        
        public Vector2 TargetPosition { get; set; }
        
        public Vector2 TargetVelocity { get; set; }
        
        public CustomNetworkTransform(uint netId) : base(netId) {}

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                writer.StartMessage(0);
            }
            
            writer.Write(this.LastSequenceId);
            writer.Write(this.TargetPosition);
            writer.Write(this.TargetVelocity);

            if (isSpawning)
            {
                writer.EndMessage();
            }
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            if (isSpawning)
            {
                reader = reader.ReadMessage();
            }

            this.LastSequenceId = reader.ReadUInt16();
            this.TargetPosition = reader.ReadVector2();
            this.TargetVelocity = reader.ReadVector2();
        }
    }
}
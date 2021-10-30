using System.Numerics;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class CustomNetworkTransform : InnerNetObject<CustomNetworkTransform>
    {
        public ushort LastSequenceId { get; set; }
        
        public Vector2 TargetPosition { get; set; }
        
        public Vector2 TargetVelocity { get; set; }

        public CustomNetworkTransform(uint netId, int ownerId = -2) : base(netId, ownerId)
        {
            this.LastSequenceId = 0;
            this.TargetPosition = Vector2.Zero;
            this.TargetVelocity = Vector2.Zero;
        }

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            writer.Write(this.LastSequenceId);
            writer.Write(this.TargetPosition);
            writer.Write(this.TargetVelocity);
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            this.LastSequenceId = reader.ReadUInt16();
            this.TargetPosition = reader.ReadVector2();
            this.TargetVelocity = reader.ReadVector2();
        }
    }
}
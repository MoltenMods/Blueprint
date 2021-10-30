using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class PlayerPhysics : InnerNetObject<PlayerPhysics>
    {
        public PlayerPhysics(uint netId, int ownerId = -2) : base(netId, ownerId) {}
        
        protected override void Write(IMessageWriter writer, bool isSpawning) {}

        protected override void Read(IMessageReader reader, bool isSpawning) {}
    }
}
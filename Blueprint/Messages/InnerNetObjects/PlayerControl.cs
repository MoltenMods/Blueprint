using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class PlayerControl : InnerNetObject<PlayerControl>
    {
        public override SpawnType? SpawnType => Enums.SpawnType.PlayerControl;
        
        public byte PlayerId { get; set; }
        
        public bool IsNew { get; set; }
        
        public PlayerPhysics Physics { get; }
        
        public CustomNetworkTransform NetTransform { get; }

        public PlayerControl(
            uint netId,
            uint physicsNetId,
            uint netTransformNetId,
            int ownerId = -2) : base(netId, ownerId)
        {
            this.Physics = new PlayerPhysics(physicsNetId);
            this.NetTransform = new CustomNetworkTransform(netTransformNetId);
            
            this.Components.Add(this.Physics);
            this.Components.Add(this.NetTransform);
        }

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                writer.Write(this.IsNew);
            }
            
            writer.Write(this.PlayerId);
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            if (isSpawning)
            {
                this.IsNew = reader.ReadBoolean();
            }

            this.PlayerId = reader.ReadByte();
        }
    }
}
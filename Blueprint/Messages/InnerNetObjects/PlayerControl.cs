using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class PlayerControl : InnerNetObject<PlayerControl>
    {
        public byte PlayerId { get; set; }
        
        public bool IsNew { get; set; }
        
        public PlayerPhysics Physics { get; }
        
        public CustomNetworkTransform NetTransform { get; }

        public PlayerControl(uint netId) : base(netId, Enums.SpawnType.PlayerControl)
        {
            this.Physics = new PlayerPhysics(netId);
        }

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                writer.StartMessage(0);
                writer.Write(this.IsNew);
            }
            
            writer.Write(this.PlayerId);

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
                this.IsNew = reader.ReadBoolean();
            }

            this.PlayerId = reader.ReadByte();
        }
    }
}
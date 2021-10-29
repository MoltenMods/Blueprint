using System.Linq;
using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Blueprint.Utilities;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class SkeldShipStatus : InnerNetObject<SkeldShipStatus>
    {
        public override SpawnType? SpawnType => Enums.SpawnType.SkeldShipStatus;
        
        public SystemType[] SystemTypes { get; }
        
        public SkeldShipStatus(uint netId, SystemType[] systemTypes, int ownerId = -2) : base(netId, ownerId)
        {
            this.SystemTypes = systemTypes;
        }

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                
            }

            var bitfield = (uint) this.SystemTypes.Select(type => (int) type).ToBitfield();

            writer.WritePacked(bitfield);
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            // this.SystemTypes = 
        }
    }
}
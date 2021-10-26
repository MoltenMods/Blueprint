using System.Linq;
using Blueprint.Enums.Networking;
using Blueprint.Utilities;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class SkeldShipStatus : InnerNetObject<SkeldShipStatus>
    {
        public SystemType[] SystemTypes { get; }
        
        public SkeldShipStatus(uint netId, SystemType[] systemTypes) : base(netId, Enums.SpawnType.SkeldShipStatus)
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
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.SystemTypes
{
    public class MedScanSystem : System
    {
        public override SystemType SystemType => SystemType.MedBay;
        
        public byte[] QueuedPlayers { get; set; }

        protected override void Write(IMessageWriter writer)
        {
            writer.WritePacked((uint) this.QueuedPlayers.Length);
            foreach (var queuedPlayer in this.QueuedPlayers)
            {
                writer.Write(queuedPlayer);
            }
        }

        protected override void Read(IMessageReader reader)
        {
            var queuedPlayersLength = reader.ReadPackedUInt32();
            this.QueuedPlayers = new byte[queuedPlayersLength];
            for (var i = 0; i < queuedPlayersLength; i++)
            {
                this.QueuedPlayers[i] = reader.ReadByte();
            }
        }
    }
}
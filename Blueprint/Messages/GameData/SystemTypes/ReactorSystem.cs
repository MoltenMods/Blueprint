using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.SystemTypes
{
    public class ReactorSystem : System
    {
        public override SystemType SystemType => SystemType.Reactor;
        
        public float Timer { get; set; }

        public UserConsolePair[] PlayersRepairing { get; set; }

        protected override void Write(IMessageWriter writer)
        {
            writer.Write(this.Timer);
            
            writer.WritePacked((uint) this.PlayersRepairing.Length);
            foreach (var playerRepairing in this.PlayersRepairing)
            {
                writer.Write(playerRepairing.PlayerId);
                writer.Write(playerRepairing.ConsoleId);
            }
        }

        protected override void Read(IMessageReader reader)
        {
            this.Timer = reader.ReadSingle();

            var playersRepairingLength = reader.ReadPackedUInt32();
            this.PlayersRepairing = new UserConsolePair[playersRepairingLength];
            for (var i = 0; i < playersRepairingLength; i++)
            {
                this.PlayersRepairing[i] = new UserConsolePair(reader.ReadByte(), reader.ReadByte());
            }
        }
    }

    public readonly struct UserConsolePair
    {
        public readonly byte PlayerId;
        public readonly byte ConsoleId;

        public UserConsolePair(byte playerId, byte consoleId)
        {
            this.PlayerId = playerId;
            this.ConsoleId = consoleId;
        }
    }
}
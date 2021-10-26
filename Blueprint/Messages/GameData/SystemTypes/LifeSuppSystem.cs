using Blueprint.Enums.Networking;
using Blueprint.Enums.Networking.ConsoleType;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.SystemTypes
{
    public class LifeSuppSystem : System
    {
        public override SystemType SystemType => SystemType.O2;
        
        public float Timer { get; set; }
        
        public O2Type[] FixedConsoles { get; set; }

        protected override void Write(IMessageWriter writer)
        {
            writer.Write(this.Timer);
            
            writer.WritePacked((uint) this.FixedConsoles.Length);
            foreach (var fixedConsole in this.FixedConsoles)
            {
                writer.WritePacked((uint) fixedConsole);
            }
        }

        protected override void Read(IMessageReader reader)
        {
            this.Timer = reader.ReadSingle();

            var fixedConsolesLength = reader.ReadPackedUInt32();
            this.FixedConsoles = new O2Type[fixedConsolesLength];
            for (var i = 0; i < fixedConsolesLength; i++)
            {
                this.FixedConsoles[i] = (O2Type) reader.ReadPackedUInt32();
            }
        }
    }
}
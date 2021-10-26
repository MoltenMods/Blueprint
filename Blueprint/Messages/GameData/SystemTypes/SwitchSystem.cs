using System.Collections;
using Blueprint.Enums.Networking;
using Blueprint.Utilities;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.SystemTypes
{
    public class SwitchSystem : System
    {
        public override SystemType SystemType => SystemType.Electrical;

        public BitArray ExpectedSwitches { get; set; } = new BitArray(5);

        public BitArray ActualSwitches { get; set; } = new BitArray(5);
        
        public byte TaskSabotagedValue { get; set; }

        protected override void Write(IMessageWriter writer)
        {
            writer.Write((byte) this.ExpectedSwitches.ToBitfield());
            writer.Write((byte) this.ActualSwitches.ToBitfield());
            
            writer.Write(this.TaskSabotagedValue);
        }

        protected override void Read(IMessageReader reader)
        {
            this.ExpectedSwitches = reader.ReadByte().ToBitArray();
            this.ActualSwitches = reader.ReadByte().ToBitArray();

            this.TaskSabotagedValue = reader.ReadByte();
        }
    }
}
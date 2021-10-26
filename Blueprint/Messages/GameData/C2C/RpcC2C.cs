using System;
using Blueprint.Enums;
using Blueprint.Messages.GameData.Rpcs;
using Blueprint.Messages.InnerNetObjects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData.C2C
{
    public class RpcC2C : GameData
    {
        public override GameDataType GameDataType => GameDataType.Rpc;
        
        public InnerNetObject InnerNetObject { get; set; }
        
        public Rpc Rpc { get; set; }

        public RpcC2C(InnerNetObject innerNetObject, Rpc rpc)
        {
            this.InnerNetObject = innerNetObject;
            this.Rpc = rpc;
        }

        protected override void Write(IMessageWriter writer)
        {
            if (!this.InnerNetObject.NetId.HasValue)
            {
                throw new Exception("Attempted to serialize InnerNetObject without a net id");
            }
            
            writer.WritePacked(this.InnerNetObject.NetId.Value);
            writer.Write((byte) this.Rpc.RpcType);
            
            this.Rpc.Serialize(writer);
            
            // TODO: finish this?
        }

        protected override void Read(IMessageReader reader)
        {
            // TODO: Rpc Read
        }
    }
}
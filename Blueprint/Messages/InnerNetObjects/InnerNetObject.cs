using System;
using Blueprint.Enums;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public abstract class InnerNetObject<T> : InnerNetObject where T : InnerNetObject<T>
    {
        protected InnerNetObject(uint? netId = null, SpawnType? spawnType = null) : base(netId, spawnType) {}
        
        public static InnerNetObject<T> CreateFrom(IMessageReader reader, bool isSpawning)
        {
            var netId = reader.ReadPackedUInt32();
            var innerNetObject = (T) Activator.CreateInstance(typeof(T), netId);
            innerNetObject!.Deserialize(reader, isSpawning);

            return innerNetObject;
        }
    }
    
    public abstract class InnerNetObject
    {
        public uint? NetId { get; }
        
        public SpawnType? SpawnType { get; }

        protected InnerNetObject(uint? netId = null, SpawnType? spawnType = null)
        {
            this.NetId = netId;
            this.SpawnType = spawnType;
        }

        public void Serialize(IMessageWriter writer, bool isSpawning)
        {
            if (!this.NetId.HasValue)
            {
                throw new Exception("Attempted to serialize InnerNetObject without a net id");
            }
            
            writer.WritePacked(this.NetId.Value);
            this.Write(writer, isSpawning);
        }

        public void Deserialize(IMessageReader reader, bool isSpawning)
        {
            this.Read(reader, isSpawning);
        }

        protected abstract void Write(IMessageWriter writer, bool isSpawning);

        protected abstract void Read(IMessageReader reader, bool isSpawning);
    }
}
using System;
using System.Collections.Generic;
using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public abstract class InnerNetObject<T> : InnerNetObject where T : InnerNetObject<T>
    {
        protected InnerNetObject(uint netId, int ownerId = -2) : base(netId, ownerId) {}
        
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
        public virtual SpawnType? SpawnType => null;
        
        public uint NetId { get; }
        
        public int OwnerId { get; }

        public List<InnerNetObject> Components { get; }

        protected InnerNetObject(uint netId, int ownerId = -2)
        {
            this.NetId = netId;

            this.OwnerId = ownerId;

            this.Components = new List<InnerNetObject>()
            {
                this
            };
        }

        public void Serialize(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                writer.StartMessage(0);
            }
            
            this.Write(writer, isSpawning);

            if (isSpawning)
            {
                writer.EndMessage();
            }
        }

        public void Deserialize(IMessageReader reader, bool isSpawning)
        {
            this.Read(isSpawning ? reader.ReadMessage() : reader, isSpawning);
        }

        public void WriteDataMessage(IMessageWriter writer)
        {
            writer.StartMessage((byte) GameDataType.Data);
            
            writer.WritePacked(this.NetId);
            this.Serialize(writer, false);
            
            writer.EndMessage();
        }

        public void WriteSpawnMessage(IMessageWriter writer, SpawnFlags spawnFlags)
        {
            writer.StartMessage((byte) GameDataType.Spawn);

            if (this.SpawnType == null)
            {
                throw new Exception($"Cannot spawn {this.GetType().Name} as it has no spawn type");
            }
            
            writer.WritePacked((uint) this.SpawnType);
            writer.WritePacked(this.OwnerId);
            writer.Write((byte) spawnFlags);
            
            writer.WritePacked(this.Components.Count);
            foreach (var component in this.Components)
            {
                writer.WritePacked(this.NetId);
                component.Serialize(writer, true);
            }
            
            writer.EndMessage();
        }

        public void WriteDespawnMessage(IMessageWriter writer)
        {
            writer.StartMessage((byte) GameDataType.Despawn);
            
            writer.WritePacked(this.NetId);
            
            writer.EndMessage();
        }

        public void StartRpcMessage(IMessageWriter writer)
        {
            writer.StartMessage((byte) GameDataType.Rpc);
            
            writer.WritePacked(this.NetId);
        }

        protected abstract void Write(IMessageWriter writer, bool isSpawning);

        protected abstract void Read(IMessageReader reader, bool isSpawning);
    }
}
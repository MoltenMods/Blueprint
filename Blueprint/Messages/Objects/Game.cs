using System;
using System.Net;
using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.Objects
{
    public class Game
    {
        public IPAddress IpAddress { get; set; }

        public ushort Port { get; set; }

        public GameCode GameCode { get; set; }

        public MapType MapType { get; set; }

        public string HostName { get; set; }

        public int PlayerCount { get; set; }

        public int ImpostorCount { get; set; }

        public int MaxPlayerCount { get; set; }

        public uint Age
        {
            get => (uint) (DateTimeOffset.UtcNow.ToUnixTimeSeconds() - this._createdAt);
            set => this._createdAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - value;
        }
        
        public GameState GameState { get; set; }

        private long _createdAt;

        public Game(uint? age = null)
        {
            this._createdAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - (age ?? 0);
        }

        public static Game CreateFrom(IMessageReader reader)
        {
            var game = new Game();
            game.Deserialize(reader);
            return game;
        }

        public void Serialize(IMessageWriter writer)
        {
            // tag is ignored so value does not matter
            writer.StartMessage(0);
                    
            writer.Write(this.IpAddress);
            writer.Write(this.Port);
            writer.Write(this.GameCode.Value);
            writer.Write(this.HostName);
            writer.Write((byte) this.PlayerCount);
            writer.WritePacked(this.Age);
            writer.Write((byte) this.MapType);
            writer.Write((byte) this.ImpostorCount);
            writer.Write((byte) this.MaxPlayerCount);
                    
            writer.EndMessage();
        }

        public void Deserialize(IMessageReader reader)
        {
            this.IpAddress = new IPAddress(reader.ReadBytes(4).Span);
            this.Port = reader.ReadUInt16();
            this.GameCode = GameCode.CreateFrom(reader);
            this.HostName = reader.ReadString();
            this.PlayerCount = reader.ReadByte();
            this.Age = reader.ReadPackedUInt32();
            this.MapType = (MapType) reader.ReadByte();
            this.ImpostorCount = reader.ReadByte();
            this.MaxPlayerCount = reader.ReadByte();
        }
    }
}
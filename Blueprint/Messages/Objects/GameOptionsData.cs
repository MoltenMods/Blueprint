using System;
using System.IO;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.Objects
{
    public class GameOptionsData
    {
        public byte Version { get; set; }
        
        public int MaxPlayers { get; set; }
        
        public GameKeywords Keywords { get; set; }
        
        public byte MapId { get; set; }

        public float PlayerSpeedMod { get; set; }
        
        public float CrewLightMod { get; set; }
        
        public float ImpostorLightMod { get; set; }
        
        public float KillCooldown { get; set; }
        
        public int NumCommonTasks { get; set; }
        
        public int NumLongTasks { get; set; }
        
        public int NumShortTasks { get; set; }
        
        public int NumEmergencyMeetings { get; set; }
        
        public int NumImpostors { get; set; }
        
        public int KillDistance { get; set; }
        
        public int DiscussionTime { get; set; }
        
        public int VotingTime { get; set; }
        
        public bool IsDefaults { get; set; }
        
        public int EmergencyCooldown { get; set; }
        
        public bool ConfirmImpostor { get; set; }
        
        public bool VisualTasks { get; set; }
        
        public bool AnonymousVotes { get; set; }
        
        public TaskBarMode TaskBarMode { get; set; }

        public static GameOptionsData CreateFrom(IMessageReader reader)
        {
            var options = new GameOptionsData();
            options.Deserialize(reader.ReadBytesAndSize());
            return options;
        }

        public void Serialize(IMessageWriter writer)
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);
            
            binaryWriter.Write(this.Version);
            binaryWriter.Write((byte) this.MaxPlayers);
            binaryWriter.Write((uint) this.Keywords);
            binaryWriter.Write(this.MapId);
            binaryWriter.Write(this.PlayerSpeedMod);
            binaryWriter.Write(this.CrewLightMod);
            binaryWriter.Write(this.ImpostorLightMod);
            binaryWriter.Write(this.KillCooldown);
            binaryWriter.Write((byte) this.NumCommonTasks);
            binaryWriter.Write((byte) this.NumLongTasks);
            binaryWriter.Write((byte) this.NumShortTasks);
            binaryWriter.Write(this.NumEmergencyMeetings);
            binaryWriter.Write((byte) this.NumImpostors);
            binaryWriter.Write((byte) this.KillDistance);
            binaryWriter.Write(this.DiscussionTime);
            binaryWriter.Write(this.VotingTime);
            binaryWriter.Write(this.IsDefaults);

            if (this.Version > 1)
            {
                binaryWriter.Write((byte) this.EmergencyCooldown);
            }

            if (this.Version > 2)
            {
                binaryWriter.Write(this.ConfirmImpostor);
                binaryWriter.Write(this.VisualTasks);
            }

            if (this.Version > 3)
            {
                binaryWriter.Write(this.AnonymousVotes);
                binaryWriter.Write((byte) this.TaskBarMode);
            }
            
            writer.WriteBytesAndSize(memoryStream.ToArray());
        }

        public void Deserialize(ReadOnlyMemory<byte> memory)
        {
            using var memoryStream = new MemoryStream(memory.ToArray());
            using var binaryReader = new BinaryReader(memoryStream);

            this.Version = binaryReader.ReadByte();

            this.MaxPlayers = binaryReader.ReadByte();
            this.Keywords = (GameKeywords) binaryReader.ReadUInt32();
            this.MapId = binaryReader.ReadByte();
            this.PlayerSpeedMod = binaryReader.ReadSingle();
            this.CrewLightMod = binaryReader.ReadSingle();
            this.ImpostorLightMod = binaryReader.ReadSingle();
            this.KillCooldown = binaryReader.ReadSingle();
            this.NumCommonTasks = binaryReader.ReadByte();
            this.NumLongTasks = binaryReader.ReadByte();
            this.NumShortTasks = binaryReader.ReadByte();
            this.NumEmergencyMeetings = binaryReader.ReadInt32();
            this.NumImpostors = binaryReader.ReadByte();
            this.KillDistance = binaryReader.ReadByte();
            this.DiscussionTime = binaryReader.ReadInt32();
            this.VotingTime = binaryReader.ReadInt32();
            this.IsDefaults = binaryReader.ReadBoolean();

            if (this.Version > 1)
            {
                this.EmergencyCooldown = binaryReader.ReadByte();
            }

            if (this.Version > 2)
            {
                this.ConfirmImpostor = binaryReader.ReadBoolean();
                this.VisualTasks = binaryReader.ReadBoolean();
            }

            if (this.Version > 3)
            {
                this.AnonymousVotes = binaryReader.ReadBoolean();
                this.TaskBarMode = (TaskBarMode) binaryReader.ReadByte();
            }
        }
    }
}
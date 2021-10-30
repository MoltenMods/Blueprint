using System.Linq;
using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class GameData : InnerNetObject<GameData>
    {
        public override SpawnType? SpawnType => Enums.SpawnType.GameData;
        
        public PlayerInfo[] Players { get; set; }
        
        public VoteBanSystem VoteBanSystem { get; }

        public GameData(uint netId, uint voteBanSystemNetId, int ownerId = -2) : base(netId, ownerId)
        {
            this.VoteBanSystem = new VoteBanSystem(voteBanSystemNetId, ownerId);
            
            this.Components.Add(this.VoteBanSystem);
        }

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            if (isSpawning)
            {
                writer.WritePacked((uint) this.Players.Length);
            }

            foreach (var player in this.Players)
            {
                if (isSpawning)
                {
                    writer.Write(player.PlayerId);
                }
                else
                {
                    writer.StartMessage(player.PlayerId);
                }

                writer.Write(player.Name);
                writer.WritePacked(player.ColorId);
                writer.WritePacked(player.HatId);
                writer.WritePacked(player.PetId);
                writer.WritePacked(player.SkinId);
                writer.Write((byte) player.PlayerStates);
                
                writer.Write((byte) player.Tasks.Length);
                foreach (var task in player.Tasks)
                {
                    writer.WritePacked(task.TaskId);
                    writer.Write(task.IsCompleted);
                }
                
                if (!isSpawning)
                {
                    writer.EndMessage();
                }
            }
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            if (isSpawning)
            {
                var playersLength = reader.ReadPackedUInt32();
                this.Players = new PlayerInfo[playersLength];
                for (var i = 0; i < playersLength; i++)
                {
                    this.Players[i] = PlayerInfo.CreateFrom(reader, reader.ReadByte());
                }
            }
            else
            {
                while (reader.Position < reader.Length)
                {
                    var playerReader = reader.ReadMessage();
                    var player = this.Players.FirstOrDefault(player => player.PlayerId == playerReader.Tag);

                    if (player == null)
                    {
                        // Player does not exist, so ignore
                        return;
                    }
                    
                    player.Deserialize(playerReader);
                }
            }
        }
    }

    public class PlayerInfo
    {
        public byte PlayerId { get; set; }
        
        public string Name { get; set; }
        
        public uint ColorId { get; set; }
        
        public uint HatId { get; set; }
        
        public uint PetId { get; set; }
        
        public uint SkinId { get; set; }
        
        public PlayerStates PlayerStates { get; set; }
        
        public TaskInfo[] Tasks { get; set; }

        public PlayerInfo(byte playerId)
        {
            this.PlayerId = playerId;
        }

        public static PlayerInfo CreateFrom(IMessageReader reader, byte playerId)
        {
            var playerInfo = new PlayerInfo(playerId);
            playerInfo.Deserialize(reader);

            return playerInfo;
        }

        public void Serialize(IMessageWriter writer)
        {
            writer.Write(this.Name);
            writer.WritePacked(this.ColorId);
            writer.WritePacked(this.HatId);
            writer.WritePacked(this.PetId);
            writer.WritePacked(this.SkinId);
            writer.Write((byte) this.PlayerStates);
            
            writer.Write((byte) this.Tasks.Length);
            foreach (var task in this.Tasks)
            {
                task.Serialize(writer);
            }
        }

        public void Deserialize(IMessageReader reader)
        {
            this.Name = reader.ReadString();
            this.ColorId = reader.ReadPackedUInt32();
            this.HatId = reader.ReadPackedUInt32();
            this.PetId = reader.ReadPackedUInt32();
            this.SkinId = reader.ReadPackedUInt32();
            this.PlayerStates = (PlayerStates) reader.ReadByte();

            var tasksLength = reader.ReadByte();
            this.Tasks = new TaskInfo[tasksLength];
            for (var i = 0; i < tasksLength; i++)
            {
                this.Tasks[i] = TaskInfo.CreateFrom(reader);
            }
        }
    }

    public class TaskInfo
    {
        public uint TaskId { get; set; }
        
        public bool IsCompleted { get; set; }

        public static TaskInfo CreateFrom(IMessageReader reader)
        {
            var taskInfo = new TaskInfo();
            taskInfo.Deserialize(reader);
            
            return taskInfo;
        }

        public void Serialize(IMessageWriter writer)
        {
            writer.WritePacked(this.TaskId);
            writer.Write(this.IsCompleted);
        }

        public void Deserialize(IMessageReader reader)
        {
            this.TaskId = reader.ReadPackedUInt32();
            this.IsCompleted = reader.ReadBoolean();
        }
    }
}
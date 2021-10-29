using System.Linq;
using Blueprint.Enums;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class MeetingHud : InnerNetObject<MeetingHud>
    {
        public override SpawnType? SpawnType => Enums.SpawnType.MeetingHud;
        
        public PlayerState[] PlayerStates { get; set; }
        
        public MeetingHud(uint netId, int ownerId = -2) : base(netId, ownerId) {}

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            writer.WritePacked(this.PlayerStates.Length);
            foreach (var playerState in this.PlayerStates)
            {
                writer.StartMessage(playerState.TargetPlayerId);
                
                playerState.Serialize(writer);
                
                writer.EndMessage();
            }
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            var playerStatesLength = reader.ReadPackedInt32();
            this.PlayerStates = new PlayerState[playerStatesLength];
            for (var i = 0; i < playerStatesLength; i++)
            {
                var playerStateReader = reader.ReadMessage();
                var playerState = this.PlayerStates.FirstOrDefault(
                    voteArea => voteArea.TargetPlayerId == playerStateReader.Tag);

                if (playerState == null)
                {
                    continue;
                }
                
                playerState.Deserialize(playerStateReader);
            }
        }

        public class PlayerState
        {
            public byte TargetPlayerId { get; set; }
            public byte VotedFor { get; set; }
            public bool DidReport { get; set; }

            public PlayerState CreateFrom(IMessageReader reader)
            {
                var playerState = new PlayerState();
                playerState.Deserialize(reader);

                return playerState;
            }

            public void Serialize(IMessageWriter writer)
            {
                writer.Write(this.VotedFor);
                writer.Write(this.DidReport);
            }

            public void Deserialize(IMessageReader reader)
            {
                this.VotedFor = reader.ReadByte();
                this.DidReport = reader.ReadBoolean();
            }
        }
        
        public struct VoterState
        {
            public byte VoterId;
            public byte VotedForId;

            public VoterState(byte voterId, byte votedForId)
            {
                this.VoterId = voterId;
                this.VotedForId = votedForId;
            }

            public static VoterState CreateFrom(IMessageReader reader)
            {
                var voterState = new VoterState();
                voterState.Deserialize(reader);

                return voterState;
            }
            
            public void Serialize(IMessageWriter writer)
            {
                writer.StartMessage(this.VoterId);
                
                writer.Write(this.VotedForId);
                
                writer.EndMessage();
            }

            public void Deserialize(IMessageReader reader)
            {
                var voterStateReader = reader.ReadMessage();

                this.VoterId = voterStateReader.Tag;
                this.VotedForId = voterStateReader.ReadByte();
            }
        }
    }
}
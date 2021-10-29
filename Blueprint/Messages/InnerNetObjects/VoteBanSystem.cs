using System.Linq;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class VoteBanSystem : InnerNetObject<VoteBanSystem>
    {
        public PlayerKickVote[] PlayerKickVotes { get; set; }

        public VoteBanSystem(uint netId, int ownerId = -2) : base(netId, ownerId) {}

        protected override void Write(IMessageWriter writer, bool isSpawning)
        {
            writer.Write((byte) this.PlayerKickVotes.Length);
            foreach (var playerKickVote in this.PlayerKickVotes)
            {
                writer.Write(playerKickVote.PlayerId);

                foreach (var votedPlayer in playerKickVote.VotedPlayers)
                {
                    writer.WritePacked(votedPlayer);
                }
            }
        }

        protected override void Read(IMessageReader reader, bool isSpawning)
        {
            var playerKickVotesLength = reader.ReadByte();
            this.PlayerKickVotes = new PlayerKickVote[playerKickVotesLength];
            for (var i = 0; i < playerKickVotesLength; i++)
            {
                this.PlayerKickVotes[i] = new PlayerKickVote(
                    reader.ReadUInt32(),
                    new []
                    {
                        reader.ReadPackedUInt32(),
                        reader.ReadPackedUInt32(),
                        reader.ReadPackedUInt32()
                    });
            }
        }
    }

    public readonly struct PlayerKickVote
    {
        public readonly uint PlayerId;
        public readonly uint[] VotedPlayers;

        public PlayerKickVote(uint playerId, uint[] votedPlayers = null)
        {
            this.PlayerId = playerId;
            this.VotedPlayers = votedPlayers?.Take(3).ToArray() ?? new uint[3];
        }
    }
}
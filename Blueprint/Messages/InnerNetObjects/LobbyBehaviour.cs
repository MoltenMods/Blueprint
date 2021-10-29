using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.InnerNetObjects
{
    public class LobbyBehaviour : InnerNetObject<LobbyBehaviour>
    {
        public LobbyBehaviour(uint netId, int ownerId = -2) : base(netId, ownerId) {}
        
        protected override void Write(IMessageWriter writer, bool isSpawning) {}

        protected override void Read(IMessageReader reader, bool isSpawning) {}
    }
}
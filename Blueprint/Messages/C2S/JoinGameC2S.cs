using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2S
{
    public static class JoinGameC2S
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode)
        {
            writer.StartMessage((byte) MessageType.JoinGame);
            
            writer.Write(gameCode.Value);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out GameCode gameCode)
        {
            gameCode = GameCode.CreateFrom(reader);
        }
    }
}
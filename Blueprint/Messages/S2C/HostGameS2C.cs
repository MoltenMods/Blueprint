using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class HostGameS2C
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode)
        {
            writer.StartMessage((byte) MessageType.HostGame);
            
            writer.Write(gameCode.Value);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out GameCode gameCode)
        {
            gameCode = GameCode.CreateFrom(reader);
        }
    }
}
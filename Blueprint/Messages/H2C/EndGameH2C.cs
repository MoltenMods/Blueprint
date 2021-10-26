using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.H2C
{
    public static class EndGameH2C
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            GameOverReason gameOverReason,
            bool showAd)
        {
            writer.StartMessage((byte) MessageType.EndGame);
            
            writer.Write(gameCode.Value);
            writer.Write((byte) gameOverReason);
            writer.Write(showAd);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out GameOverReason gameOverReason,
            out bool showAd)
        {
            gameCode = GameCode.CreateFrom(reader);
            gameOverReason = (GameOverReason) reader.ReadByte();
            showAd = reader.ReadBoolean();
        }
    }
}
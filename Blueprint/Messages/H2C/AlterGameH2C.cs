using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.H2C
{
    public static class AlterGameH2C
    {
        public static void Serialize(IMessageWriter writer, GameCode gameCode, AlterGameTag alterGameTag, byte value)
        {
            writer.StartMessage((byte) MessageType.AlterGame);
            
            writer.Write(gameCode.Value);
            writer.Write((byte) alterGameTag);
            writer.Write(value);
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out AlterGameTag alterGameTag,
            out byte value)
        {
            gameCode = GameCode.CreateFrom(reader);
            alterGameTag = (AlterGameTag) reader.ReadByte();
            value = reader.ReadByte();
        }
    }
}
using Blueprint.Messages.Objects;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2S
{
    public static class ReportPlayerC2S
    {
        public static void Serialize(
            IMessageWriter writer,
            GameCode gameCode,
            uint reportedPlayerId,
            ReportReason reportReason)
        {
            writer.StartMessage((byte) MessageType.ReportPlayer);
            
            writer.Write(gameCode.Value);
            writer.WritePacked(reportedPlayerId);
            writer.Write((byte) reportReason);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameCode gameCode,
            out uint reportedPlayerId,
            out ReportReason reportReason)
        {
            gameCode = GameCode.CreateFrom(reader);
            reportedPlayerId = reader.ReadPackedUInt32();
            reportReason = (ReportReason) reader.ReadByte();
        }
    }
}
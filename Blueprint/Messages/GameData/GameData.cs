using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.GameData
{
    public static class GameData
    {
        public static void StartGameDataMessage(IMessageWriter writer, GameCode gameCode, int targetPlayerId = -1)
        {
            if (targetPlayerId >= 0)
            {
                writer.StartMessage((byte) MessageType.GameDataTo);
                writer.Write(gameCode.Value);
                writer.WritePacked(targetPlayerId);
            }
            else
            {
                writer.StartMessage((byte) MessageType.GameData);
                writer.Write(gameCode.Value);
            }
        }
    }
}
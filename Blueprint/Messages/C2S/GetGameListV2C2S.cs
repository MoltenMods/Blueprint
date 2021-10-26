using Blueprint.Enums.Networking;
using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2S
{
    public static class GetGameListV2C2S
    {
        public static void Serialize(IMessageWriter writer, GameOptionsData options, QuickChatMode chatMode)
        {
            writer.StartMessage((byte) MessageType.GetGameListV2);
            
            // Hardcoded for some reason
            writer.WritePacked(2);
            
            options.Serialize(writer);
            writer.Write((byte) chatMode);
            
            writer.EndMessage();
        }

        public static void Deserialize(
            IMessageReader reader,
            out GameOptionsData gameOptionsData,
            out QuickChatMode chatMode)
        {
            gameOptionsData = GameOptionsData.CreateFrom(reader);
            chatMode = (QuickChatMode) reader.ReadByte();
        }
    }
}
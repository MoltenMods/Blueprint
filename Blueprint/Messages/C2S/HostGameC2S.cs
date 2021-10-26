using Blueprint.Enums.Networking;
using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2S
{
    public static class HostGameC2S
    {
        public static void Serialize(IMessageWriter writer, GameOptionsData options, QuickChatMode chatMode)
        {
            writer.StartMessage((byte) MessageType.HostGame);
            
            options.Serialize(writer);
            writer.Write((byte) chatMode);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out GameOptionsData options, out QuickChatMode chatMode)
        {
            options = GameOptionsData.CreateFrom(reader);
            chatMode = (QuickChatMode) reader.ReadByte();
        }
    }
}
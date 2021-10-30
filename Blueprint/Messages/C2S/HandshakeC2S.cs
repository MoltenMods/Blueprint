using System.IO;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.C2S
{
    public static class HandshakeC2S
    {
        public static byte[] Serialize(
            int broadcastVersion,
            string playerName,
            uint authNonce,
            GameKeywords language,
            QuickChatMode chatMode)
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);
            
            binaryWriter.Write(broadcastVersion);
            binaryWriter.Write(playerName);
            binaryWriter.Write(authNonce);
            binaryWriter.Write((uint) language);
            binaryWriter.Write((byte) chatMode);

            return memoryStream.ToArray();
        }

        public static void Deserialize(
            IMessageReader reader,
            out int broadcastVersion,
            out string playerName,
            out uint authNonce,
            out GameKeywords language,
            out QuickChatMode chatMode)
        {
            broadcastVersion = reader.ReadInt32();
            playerName = reader.ReadString();
            authNonce = reader.ReadUInt32();
            language = (GameKeywords) reader.ReadByte();
            chatMode = (QuickChatMode) reader.ReadByte();
        }
    }
}
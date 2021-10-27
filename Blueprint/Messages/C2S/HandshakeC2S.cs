using System.IO;
using Blueprint.Enums.Networking;

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
            byte[] data,
            out int broadcastVersion,
            out string playerName,
            out uint authNonce,
            out GameKeywords language,
            out QuickChatMode chatMode)
        {
            using var memoryStream = new MemoryStream(data);
            using var binaryReader = new BinaryReader(memoryStream);

            broadcastVersion = binaryReader.ReadInt32();
            playerName = binaryReader.ReadString();
            authNonce = binaryReader.ReadUInt32();
            language = (GameKeywords) binaryReader.ReadByte();
            chatMode = (QuickChatMode) binaryReader.ReadByte();
        }
    }
}
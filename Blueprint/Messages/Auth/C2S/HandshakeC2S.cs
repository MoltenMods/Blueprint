using System.IO;
using Blueprint.Enums.Networking;

namespace Blueprint.Messages.Auth.C2S
{
    public static class HandshakeC2S
    {
        public static byte[] Serialize(int broadcastVersion, Platform platform, string productUserId)
        {
            using var memoryStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(memoryStream);
            
            binaryWriter.Write(broadcastVersion);
            binaryWriter.Write((byte) platform);
            binaryWriter.Write(productUserId);

            return memoryStream.ToArray();
        }

        public static void Deserialize(
            byte[] data,
            out int broadcastVersion,
            out Platform platform,
            out string productUserId)
        {
            using var memoryStream = new MemoryStream(data);
            using var binaryReader = new BinaryReader(memoryStream);

            broadcastVersion = binaryReader.ReadInt32();
            platform = (Platform) binaryReader.ReadByte();
            productUserId = binaryReader.ReadString();
        }
    }
}
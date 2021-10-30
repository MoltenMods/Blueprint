using System.IO;
using Blueprint.Enums.Networking;
using Singularity.Hazel.Api.Net.Messages;

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
            IMessageReader reader,
            out int broadcastVersion,
            out Platform platform,
            out string productUserId)
        {
            broadcastVersion = reader.ReadInt32();
            platform = (Platform) reader.ReadByte();
            productUserId = reader.ReadString();
        }
    }
}
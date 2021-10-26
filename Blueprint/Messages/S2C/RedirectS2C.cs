using System.Net;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class RedirectS2C
    {
        public static void Serialize(IMessageWriter writer, IPAddress ipAddress, ushort port)
        {
            writer.StartMessage((byte) MessageType.Redirect);
            
            writer.Write(ipAddress);
            writer.Write(port);
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out IPAddress ipAddress, out ushort port)
        {
            ipAddress = new IPAddress(reader.ReadBytes(4).Span);
            port = reader.ReadUInt16();
        }
    }
}
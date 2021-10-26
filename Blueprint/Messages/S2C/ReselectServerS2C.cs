using System.Net;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class ReselectServerS2C
    {
        public static void Serialize(IMessageWriter writer, byte version, MasterServer[] masterServers)
        {
            writer.StartMessage((byte) MessageType.ReselectServer);
            
            writer.Write(version);
            
            writer.WritePacked((uint) masterServers.Length);
            foreach (var masterServer in masterServers)
            {
                writer.StartMessage(0);
                
                writer.Write(masterServer.Name);
                writer.Write(masterServer.IpAddress);
                writer.Write(masterServer.Port);
                writer.WritePacked(masterServer.ConnectionCount);
                
                writer.EndMessage();
            }
            
            writer.EndMessage();
        }

        public static void Deserialize(IMessageReader reader, out byte version, out MasterServer[] masterServers)
        {
            version = reader.ReadByte();

            var masterServersLength = reader.ReadPackedUInt32();
            masterServers = new MasterServer[masterServersLength];
            for (var i = 0; i < masterServersLength; i++)
            {
                var serverReader = reader.ReadMessage();
                
                masterServers[i] = new MasterServer(
                    serverReader.ReadString(),
                    new IPAddress(serverReader.ReadBytes(4).Span),
                    serverReader.ReadUInt16(),
                    serverReader.ReadPackedUInt32());
            }
        }
    }

    public readonly struct MasterServer
    {
        public readonly string Name;
        public readonly IPAddress IpAddress;
        public readonly ushort Port;
        public readonly uint ConnectionCount;

        public MasterServer(string name, IPAddress ipAddress, ushort port, uint connectionCount)
        {
            this.Name = name;
            this.IpAddress = ipAddress;
            this.Port = port;
            this.ConnectionCount = connectionCount;
        }
    }
}
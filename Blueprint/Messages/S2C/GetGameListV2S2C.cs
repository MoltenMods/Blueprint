using System.Collections.Generic;
using Blueprint.Messages.Objects;
using Singularity.Hazel.Api.Net.Messages;

namespace Blueprint.Messages.S2C
{
    public static class GetGameListV2S2C
    {
        public static void Serialize(
            IMessageWriter writer,
            Game[] games = null,
            uint? skeldGameCount = null,
            uint? miraGameCount = null,
            uint? polusGameCount = null)
        {
            writer.StartMessage((byte) MessageType.GetGameListV2);

            if (games != null)
            {
                writer.StartMessage(0);
                
                foreach (var game in games)
                {
                    game.Serialize(writer);
                }
                
                writer.EndMessage();
            }
            
            if (skeldGameCount.HasValue && miraGameCount.HasValue && polusGameCount.HasValue)
            {
                writer.StartMessage(1);
                
                writer.Write(skeldGameCount.Value);
                writer.Write(miraGameCount.Value);
                writer.Write(polusGameCount.Value);
                
                writer.EndMessage();
            }
            
            writer.EndMessage();
        }
        
        public static void Deserialize(
            IMessageReader reader,
            out Game[] games,
            out uint? skeldGameCount,
            out uint? miraGameCount,
            out uint? polusGameCount)
        {
            games = null;
            skeldGameCount = null;
            miraGameCount = null;
            polusGameCount = null;
            
            if (reader.Position == reader.Length)
            {
                return;
            }

            while (reader.Position < reader.Length)
            {
                var innerReader = reader.ReadMessage();

                switch (innerReader.Tag)
                {
                    case 0:
                    {
                        var gameList = new List<Game>();
                        
                        while (innerReader.Position < innerReader.Length)
                        {
                            var gameReader = innerReader.ReadMessage();
                            gameList.Add(Game.CreateFrom(gameReader));
                        }

                        games = gameList.ToArray();

                        break;
                    }
                    case 1:
                    {
                        skeldGameCount = innerReader.ReadUInt32();
                        miraGameCount = innerReader.ReadUInt32();
                        polusGameCount = innerReader.ReadUInt32();

                        break;
                    }
                }
            }
        }
    }
}
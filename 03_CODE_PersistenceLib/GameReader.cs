using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        public Game Read(string filePath)
        {
            string fileName = "../03_CODE_PersistenceLib/Data/TempleOfDoom.json";

            JObject json = JObject.Parse(File.ReadAllText(fileName));

            parsePlayer(json);
            parseRooms(json);
            parseItems(json);
            parseConnections(json);

            return new Game();
        }

        private void parsePlayer(JObject json)
        {
            var parsedPlayer = json["player"].ToObject<ParsedPlayer>();

            // Create player in factory
        }

        private void parseRooms(JObject json)
        {
            var parsedRooms = new List<ParsedRoom>();

            foreach (var room in json["rooms"])
            {
                var parsedRoom = room.ToObject<ParsedRoom>();
                parsedRooms.Add(parsedRoom);
            }

            // Create rooms in factory
        }

        private void parseItems(JObject json)
        {
            var parsedItems = new Dictionary<int, List<ParsedItem>>();

            foreach (var room in json["rooms"])
            {
                var parsedRoom = room.ToObject<ParsedRoom>();

                var jsonItems = json["rooms"][parsedRoom.id - 1]["items"];

                if (jsonItems == null) continue;

                var parsedItemList = new List<ParsedItem>();

                foreach (var item in jsonItems)
                {
                    var parsedItem = item.ToObject<ParsedItem>();
                    parsedItemList.Add(parsedItem);
                }

                parsedItems.Add(parsedRoom.id, parsedItemList);
            }

            // Create items in factory
        }

        private void parseConnections(JObject json)
        {
            var parsedConnections = new List<ParsedConnection>();

            foreach (var room in json["connections"])
            {
                var parsed = room.ToObject<Dictionary<string, int>>();

                var identifiers = parsed.Values.ToList();
                var sides = parsed.Keys.ToList();

                var parsedConnection = new ParsedConnection
                {
                    In = new KeyValuePair<int, Side>(identifiers[0], System.Enum.Parse<Side>(sides[0])),
                    Out = new KeyValuePair<int, Side>(identifiers[1], System.Enum.Parse<Side>(sides[1]))
                };

                parsedConnections.Add(parsedConnection);
            }

            // Create items in factory
        }
    }
}

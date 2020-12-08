using System;
using System.Collections.Generic;
using System.IO;
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

            Console.WriteLine(parsedPlayer);
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

            Console.WriteLine(parsedRooms);
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

            Console.WriteLine(parsedItems);
            // Create items in factory
        }

        private void parseConnections(JObject json)
        {
            var parsedConnections = new List<ParsedConnection>();

            foreach (var room in json["connections"])
            {
                var parsedConnection = room.ToObject<Dictionary<string, string>>();
            }

            Console.WriteLine(parsedConnections);
        }
    }
}

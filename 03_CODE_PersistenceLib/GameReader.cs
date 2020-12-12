using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_Filesystem.Models;
using CODE_GameLib;
using CODE_GameLib.Models;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        public Game Read(string filePath)
        {
            JObject json = JObject.Parse(File.ReadAllText(filePath));

            var player = this.player(json);
            var rooms = this.rooms(json);
            parseItems(json);
            parseConnections(json);
            return new Game();
        }

        private Player player(JObject json)
        {
            return json["player"].ToObject<Player>();
        }

        private List<Room> rooms(JObject json)
        {
            var parsedRooms = new List<Room>();
            foreach (var room in json["rooms"])
            {
                var parsedRoom = room.ToObject<Room>();
                parsedRooms.Add(parsedRoom);
            }
            return parsedRooms;
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

            foreach (JObject jconnection in json["connections"])
            {
                var connection = jconnection.Children().OfType<JProperty>().ToArray();
                var parsedConnection = new ParsedConnection
                {
                    In = new KeyValuePair<int, Side>(
                        connection[0].Value.ToObject<int>(),
                        System.Enum.Parse<Side>(connection[0].Name)
                    ),
                    Out = new KeyValuePair<int, Side>(
                        connection[1].Value.ToObject<int>(),
                        System.Enum.Parse<Side>(connection[1].Name)
                    )
                };

                parsedConnections.Add(parsedConnection);
            }
            // Create items in factory
        }
    }
}

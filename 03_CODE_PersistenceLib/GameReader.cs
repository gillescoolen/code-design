using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Utilities;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        public Game Read(string filePath)
        {
            JObject json = JObject.Parse(File.ReadAllText(filePath));

            var player = CreatePlayer(json);
            var rooms = CreateRooms(json, player);

            var game = new Game(player, rooms);

            GenerateConnections(game, json);

            return game;
        }

        private Player CreatePlayer(JObject json)
        {
            var jsonPlayer = json["player"];
            var startRoomId = jsonPlayer["startRoomId"].Value<int>();
            var startPosition = new Position
            {
                X = jsonPlayer["startX"]!.Value<int>(),
                Y = jsonPlayer["startY"]!.Value<int>()
            };

            return new Player(startRoomId, startPosition, jsonPlayer["lives"]!.Value<int>());
        }

        private List<Room> CreateRooms(JObject json, Player player)
        {
            var rooms = new List<Room>();

            foreach (var jsonRoom in json["rooms"])
            {
                var id = jsonRoom["id"].Value<int>();
                var width = jsonRoom["width"].Value<int>();
                var height = jsonRoom["height"].Value<int>();

                var room = new Room(width, height, id);

                if (id == player.StartRoomId)
                {
                    room.SpawnPlayer(player.StartPosition, player);
                }

                if (jsonRoom["items"] != null)
                {
                    CreateEntities(room, jsonRoom["items"]!);
                }

                rooms.Add(room);
            }

            return rooms;
        }


        private void CreateEntities(Room room, JToken items)
        {
            var factory = new Factory<Entity>("Models.Entities");

            foreach (var jsonItem in items)
            {
                var name = jsonItem["type"]!.Value<string>();
                var color = (jsonItem["color"] ?? "").Value<string>();
                var item = factory.Create(name);

                if (item == null) continue;

                var x = jsonItem["x"].Value<int>();
                var y = jsonItem["y"].Value<int>();
                var tile = room.Tiles.FirstOrDefault(tile => tile.Key.X == x && tile.Key.Y == y);

                item.Damage = (jsonItem["damage"] ?? 0).Value<int>();
                tile.Value.Entity = item;
                if (color.Length > 1) tile.Value.Entity.Color = GetConsoleColorByString(color);
            }
        }

        private void GenerateConnections(Game game, JToken json)
        {
            var factory = new Factory<Door>("Models.Doors");

            foreach (var jsonConnection in json["connections"]!)
            {
                var newConnection = new Connection();

                foreach (var property in jsonConnection.Children().OfType<JProperty>())
                {
                    var key = property.Name;
                    var value = property.Value;

                    if (key == "door")
                    {
                        var color = (value["color"] ?? "").Value<string>();
                        newConnection.Door = factory.Create($"{value["type"]} door");
                        if (newConnection.Door == null) continue;
                        if (color.Length > 1) newConnection.Door.Color = GetConsoleColorByString(color);
                        continue;
                    }

                    newConnection.Connections.Add(key.GetByName(), game.GetRoomById(value.Value<int>())!);
                }

                foreach (var connection in newConnection.Connections)
                {
                    var room = game.GetRoomById(connection.Value.Id);
                    room.AddConnection(connection.Key.GetOpposite(), newConnection);
                }
            }
        }

        private ConsoleColor GetConsoleColorByString(string color) {
            return (ConsoleColor) Enum.Parse(typeof(ConsoleColor), char.ToUpper(color[0]) + color.Substring(1));
        }
    }
}
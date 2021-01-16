using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Connectors;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Utilities;
using Newtonsoft.Json.Linq;
using CODE_TempleOfDoom_DownloadableContent;
using CODE_GameLib.Adapters;

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
            var player = json["player"];
            var startRoomId = player["startRoomId"].Value<int>();
            var startPosition = new Position
            {
                X = player["startX"]!.Value<int>(),
                Y = player["startY"]!.Value<int>()
            };

            return new Player(startRoomId, startPosition, player["lives"]!.Value<int>());
        }

        private static void PlaceEnemies(Room room, JToken enemies)
        {
            foreach (var enemy in enemies)
            {
                var parameters = new
                {
                    Type = enemy["type"]!.Value<string>(),
                    StartX = enemy["x"]!.Value<int>(),
                    StartY = enemy["y"]!.Value<int>(),
                    MinX = enemy["minX"]!.Value<int>(),
                    MaxX = enemy["maxX"]!.Value<int>(),
                    MinY = enemy["minY"]!.Value<int>(),
                    MaxY = enemy["maxY"]!.Value<int>()
                };

                dynamic actor;

                var tile = room.Tiles.FirstOrDefault(s => s.Key.X == parameters.StartX && s.Key.Y == parameters.StartY);

                if (parameters.Type == "horizontal")
                    actor = new HorizontallyMovingEnemy(1, parameters.StartX, parameters.StartY, parameters.MinX, parameters.MaxX);
                else
                    actor = new VerticallyMovingEnemy(1, parameters.StartX, parameters.StartY, parameters.MinY, parameters.MaxY);

                tile.Value.Actor = new EnemyAdapter(actor);
            }
        }


        private List<Room> CreateRooms(JObject json, Player player)
        {
            var rooms = new List<Room>();

            foreach (var room in json["rooms"])
            {
                var id = room["id"].Value<int>();
                var width = room["width"].Value<int>();
                var height = room["height"].Value<int>();

                var newRoom = new Room(width, height, id);

                if (id == player.StartRoomId) newRoom.SpawnPlayer(player.StartPosition, player);

                if (room["items"] != null) CreateEntities(newRoom, room["items"]!);

                if (room["enemies"] != null) PlaceEnemies(newRoom, room["enemies"]!);

                rooms.Add(newRoom);
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
                var entity = factory.Create(name);

                if (entity == null) continue;

                var x = jsonItem["x"].Value<int>();
                var y = jsonItem["y"].Value<int>();
                var tile = room.Tiles.FirstOrDefault(tile => tile.Key.X == x && tile.Key.Y == y);

                entity.Damage = (jsonItem["damage"] ?? 0).Value<int>();
                tile.Value.Entity = entity;
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

                    if (key == "ladder")
                    {
                        var upperPosition = new Position
                        { X = value["upperX"]!.Value<int>(), Y = value["upperY"]!.Value<int>() };
                        var lowerPosition = new Position
                        { X = value["lowerX"]!.Value<int>(), Y = value["lowerY"]!.Value<int>() };
                        newConnection.Ladder = new Ladder(upperPosition, lowerPosition);
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

        private ConsoleColor GetConsoleColorByString(string color)
        {
            return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), char.ToUpper(color[0]) + color.Substring(1));
        }
    }
}
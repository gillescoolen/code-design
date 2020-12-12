using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Dictionary<Position, Tile> Tiles { get; set; } = new Dictionary<Position, Tile>();

        public Room(int width, int height, int id)
        {
            Id = id;
            Width = width;
            Height = height;
            GenerateRoom();
        }

        private void GenerateRoom()
        {
            for (var y = 0; y <= Height - 1; ++y)
            {
                for (var x = 0; x <= Width - 1; ++x)
                {
                    var position = new Position { X = x, Y = y };
                    var tile = new Tile { Entity = IsWall(x, y) ? new Wall() : null, Position = position };
                    Tiles.Add(position, tile);
                }
            }
        }

        private bool IsWall(int x, int y)
        {
            return x == Width - 1 || y == Height - 1 || y == 0 || x == 0;
        }

        public Tile GetTileByPosition(int x, int y)
        {
            return Tiles[new Position { X = x, Y = y }];
        }

        public List<Tile> GetTilesByDoor<T>()
        {
            return Tiles.Where(tile => tile.Value.Connection?.Door is T)
                   .Select(tile => tile.Value)
                   .ToList();
        }

        public Position GetSpawnPosition(Side direction)
        {
            var position = new Position();

            if (direction == Side.NORTH || direction == Side.SOUTH)
            {
                position.X = (int)Math.Floor(Width / 2.0);
                position.Y = direction == Side.NORTH ? 0 : Height - 1;
            }
            else
            {
                position.X = direction == Side.WEST ? 0 : Width - 1;
                position.Y = (int)Math.Floor(Height / 2.0);
            }

            return position;
        }

        public Tile GetTileByItem(Entity entity)
        {
            return Tiles.First(tile => tile.Value.Entity == entity).Value;
        }

        public void AddConnection(Side direction, Connection connection)
        {
            var position = GetSpawnPosition(direction);
            var tile = GetTileByPosition(position.X, position.Y);

            tile.Entity = null;
            tile.Connection = connection;
        }

        public List<Connection> GetConnections()
        {
            return Tiles.Where(tile => tile.Value.Connection != null)
                .Select(tile => tile.Value.Connection)
                .ToList()!;
        }

        public void SpawnPlayer(Position position, Player player)
        {
            Tiles[position].Player = player;
        }

        public void RemovePlayer()
        {
            var position = GetPlayerPosition();
            if (position == null) return;

            var tile = GetTileByPosition(position.X, position.Y);
            tile.Player = null;
        }

        public Position GetPlayerPosition()
        {
            return Tiles.FirstOrDefault(e => e.Value.Player != null).Key;
        }
    }
}
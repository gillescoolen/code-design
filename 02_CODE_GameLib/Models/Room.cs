using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Adapters;
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
                    var entity = x == Width - 1 || y == Height - 1 || y == 0 || x == 0 ? new Wall() : null;

                    var tile = new Tile
                    {
                        Entity = entity,
                        Position = position
                    };

                    Tiles.Add(position, tile);
                }
            }
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

        public IEnumerable<EnemyAdapter> GetEnemies()
        {
            return Tiles.Values
                .Where(t => t.Actor is EnemyAdapter)
                .Select(t => t.Actor as EnemyAdapter)
                .ToList();
        }

        //Beweeg alle enemies in de kamer.
        public void MoveEnemies(Game game)
        {
            foreach (var enemy in GetEnemies())
            {
                var tile = enemy.Move(this, new Position());
                tile?.Entity?.Interact(enemy, this, game);
            }
        }

        public void RemoveEnemy(Position position)
        {
            var tile = GetTileByPosition(position.X, position.Y);
            tile.Actor = null;
        }

        public void PlaceActor(Position position, IActor actor)
        {
            Tiles[position].Actor = actor;
        }

        public Position GetSpawnPosition(Direction direction)
        {
            var position = new Position();

            if (direction == Direction.NORTH || direction == Direction.SOUTH)
            {
                position.X = (int)Math.Floor(Width / 2.0);
                position.Y = direction == Direction.NORTH ? 0 : Height - 1;
            }
            else
            {
                position.X = direction == Direction.WEST ? 0 : Width - 1;
                position.Y = (int)Math.Floor(Height / 2.0);
            }

            return position;
        }

        public Tile GetTileByEntity(Entity entity)
        {
            return Tiles.First(tile => tile.Value.Entity == entity).Value;
        }

        public void AddConnection(Direction direction, Connection connection)
        {
            Position position;

            if (connection.Ladder == null) position = GetSpawnPosition(direction);
            else position = connection.Ladder.GetPosition(connection.Connections.First(c => c.Value == this).Key);

            var tile = GetTileByPosition(position.X, position.Y);

            tile.Entity = null;
            tile.Connection = connection;
        }

        public void RemovePlayer()
        {
            var position = GetPlayerPosition();
            if (position == null) return;

            var tile = GetTileByPosition(position.X, position.Y);
            tile.Actor = null;
        }

        public Position GetPlayerPosition()
        {
            return Tiles.FirstOrDefault(e => e.Value.Actor is Player).Key;
        }
    }
}
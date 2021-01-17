#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Adapters
{
    public class EnemyAdapter : IActor
    {
        private readonly Enemy Enemy;
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        public int Lives { get; private set; }
        public Position CurrentPosition { get; private set; }
        public Position PreviousPosition { get; private set; }
        public List<Entity> Inventory { get; } = new List<Entity>();

        public EnemyAdapter(Enemy enemy)
        {
            Enemy = enemy;
            Lives = Enemy.NumberOfLives;
            CurrentPosition = new Position { X = Enemy.CurrentXLocation, Y = Enemy.CurrentYLocation };
            PreviousPosition = CurrentPosition;
        }

        public Tile? Move(Room room, Position movePosition)
        {
            PreviousPosition = CurrentPosition;
            Enemy.Move();

            var position = new Position
            {
                X = Enemy.CurrentXLocation,
                Y = Enemy.CurrentYLocation
            };

            var tile = room.Tiles[position];

            if (tile.Entity != null && !tile.Entity.IsInteractable())
            {
                Back();
                return null;
            }

            if (tile.Actor is Player)
            {
                Back();
                tile.Actor.Hurt(1);
                return null;
            }

            CurrentPosition = new Position { X = Enemy.CurrentXLocation, Y = Enemy.CurrentYLocation };

            room.RemoveEnemy(PreviousPosition);
            room.PlaceActor(position, this);

            return tile;
        }

        public int Hurt(int damage)
        {
            Enemy.GetHurt(damage);
            return Lives -= damage;
        }

        public List<Entity> GetInventory<T>()
        {
            return Inventory.Where(i => i is T)
                .ToList();
        }

        public void AddToInventory(Entity item)
        {
            Inventory.Add(item);
        }

        private void Back()
        {
            Enemy.CurrentXLocation = PreviousPosition.X;
            Enemy.CurrentYLocation = PreviousPosition.Y;
            CurrentPosition.X = Enemy.CurrentXLocation;
            CurrentPosition.Y = Enemy.CurrentYLocation;
        }
    }
}
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
        private readonly Enemy enemy;
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        public int Lives { get; private set; }
        public Position CurrentPosition { get; private set; }
        public Position PreviousPosition { get; private set; }
        public List<Entity> Inventory { get; } = new List<Entity>();

        public EnemyAdapter(Enemy enemy)
        {
            this.enemy = enemy;
            Lives = this.enemy.NumberOfLives;
            CurrentPosition = new Position { X = this.enemy.CurrentXLocation, Y = this.enemy.CurrentYLocation };
            PreviousPosition = CurrentPosition;
        }

        public Tile? Move(Room room, Position movePosition)
        {
            PreviousPosition = CurrentPosition;
            enemy.Move();

            var position = new Position
            {
                X = enemy.CurrentXLocation,
                Y = enemy.CurrentYLocation
            };

            var tile = room.Tiles[position];
            
            if (tile.Entity != null && !tile.Entity.IsInteractable())
            {
                ResetPosition();
                return null;
            }

            if (tile.Actor is Player)
            {
                ResetPosition();
                tile.Actor.Hurt(1);
                return null;
            }
            
            CurrentPosition = new Position { X = enemy.CurrentXLocation, Y = enemy.CurrentYLocation };

            room.RemoveEnemy(PreviousPosition);
            room.PlaceActor(position, this);

            return tile;
        }

        public int Hurt(int damage)
        {
            enemy.GetHurt(damage);
            return Lives -= damage;
        }

        public List<Entity> GetInventory<T>()
        {
            return Inventory.Where(i => i is T).ToList();
        }

        public void AddToInventory(Entity Entity)
        {
            Inventory.Add(Entity);
        }

        private void ResetPosition()
        {
            enemy.CurrentXLocation = PreviousPosition.X;
            enemy.CurrentYLocation = PreviousPosition.Y;
            CurrentPosition.X = enemy.CurrentXLocation;
            CurrentPosition.Y = enemy.CurrentYLocation;
        }
    }
}
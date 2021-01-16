using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Adapters;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Player : IActor
    {
        public List<Entity> Inventory { get; } = new List<Entity>();
        public readonly Position StartPosition;
        public int StartRoomId { get; set; }
        public int Lives { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.Blue;

        public Player(int startRoomId, Position position, int lives)
        {
            StartRoomId = startRoomId;
            StartPosition = position;
            Lives = lives;
        }

        public List<Entity> GetInventory<T>()
        {
            return Inventory.Where(i => i is T).ToList();
        }

        public Tile Move(Room room, Position movePosition)
        {
            var current = room.GetPlayerPosition()!;

            var position = new Position
            {
                X = current.X + movePosition.X,
                Y = current.Y + movePosition.Y
            };

            var currentSquare = room.Tiles[current];

            if (position.X < 0 || position.Y < 0 || position.Y >= room.Height || position.X >= room.Width)
            {
                return currentSquare;
            }

            var newSquare = room.Tiles[position];
            var connection = newSquare.Connection;

            // If the position we move to has an entity which is not interactable.
            if (newSquare.Entity != null && !newSquare.Entity.IsInteractable() || connection?.Door != null && !connection.Door.CanEnter(this)) return null;

            if (newSquare.Actor is EnemyAdapter)
            {
                Hurt(1);
                return null;
            }

            room.RemovePlayer();
            room.SpawnPlayer(position, this);

            return newSquare;
        }

        public int Hurt(int damage)
        {
            return Lives -= damage;
        }

        public void AddToInventory(Entity entity)
        {
            Inventory.Add(entity);
        }

        public int GetAmountOfSankaraStones()
        {
            return Inventory.Count(i => i is SankaraStone);
        }
    }
}
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
        public int StartRoomId { get; set; }
        public int Lives { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.Blue;
        public bool CanOpenDoors { get; set; }
        public bool IsImmortal { get; set; } = false;
        public Position CurrentPosition { get; private set; }
        public Position PreviousPosition { get; private set; }


        public Player(int startRoomId, Position position, int lives)
        {
            StartRoomId = startRoomId;
            Lives = lives;
            CurrentPosition = position;
            PreviousPosition = position;
        }

        public List<Entity> GetInventory<T>()
        {
            return Inventory.Where(i => i is T).ToList();
        }

        public Tile Move(Room room, Position movePosition)
        {
            var currentPosition = room.GetPlayerPosition()!;
            CurrentPosition = currentPosition;

            //Bepaal de mogelijk nieuwe positie
            var newPosition = new Position
            {
                X = currentPosition.X + movePosition.X,
                Y = currentPosition.Y + movePosition.Y
            };

            var currentSquare = room.Tiles[currentPosition];

            if (newPosition.X < 0 || newPosition.Y < 0 || newPosition.Y >= room.Height || newPosition.X >= room.Width)
                return currentSquare;

            var position = room.Tiles[newPosition];
            var connection = position.Connection;

            if (position.Entity != null && !position.Entity.IsInteractable() ||
                connection?.Door != null && !connection.Door.CanEnter(this)) return null;

            if (position.Actor is EnemyAdapter)
            {
                Hurt(1);
                return null;
            }

            room.RemovePlayer();
            room.PlaceActor(newPosition, this);
            PreviousPosition = CurrentPosition;
            CurrentPosition = newPosition;

            return position;
        }

        public int Hurt(int damage)
        {
            return IsImmortal ? 0 : Lives -= damage;
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
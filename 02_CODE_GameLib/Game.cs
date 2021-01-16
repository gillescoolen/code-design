using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;
using CODE_GameLib.Utilities;

namespace CODE_GameLib
{
    public class Game : Provider<Game>
    {
        private Player Player { get; set; }
        private List<Room> Rooms { get; set; } = new List<Room>();

        public Game(Player player, List<Room> rooms)
        {
            Player = player;
            Rooms = rooms;
        }

        public Room GetCurrentRoom()
        {
            return Rooms.Find(r => r.GetPlayerPosition() != null)!;
        }

        public Room GetRoomById(int id)
        {
            return Rooms.FirstOrDefault(r => r.Id == id);
        }

        public bool UseConnection(Tile tile)
        {
            if (Player != tile.Actor || tile.Connection == null) return false;

            var roomDirection = tile.Connection.Enter(GetCurrentRoom(), Player);

            if (!roomDirection.HasValue) return false;

            Notify(this);

            return true;
        }

        public int GetPlayerLives()
        {
            return Player.Lives;
        }

        public bool IsOver()
        {
            return Player.Lives == 0;
        }

        public bool HasBeenWon()
        {
            return Player.GetAmountOfSankaraStones() == 5;
        }

        public Entity UseEntity(Tile tile)
        {
            if (Player != tile.Actor || tile.Entity == null) return null;
            var entity = tile.Entity.Interact(Player, GetCurrentRoom(), this);

            Notify(this);

            return entity;
        }

        public void Move(Position movePosition, bool enemyMovement = false, bool isTurn = true)
        {
            var currentRoom = GetCurrentRoom();

            Tile? tile = null;

            if (!enemyMovement || isTurn)
                tile = Player.Move(currentRoom, movePosition);

            if (enemyMovement || isTurn)
                currentRoom.MoveEnemies(this);

            if (tile == null || !UseConnection(tile) && UseEntity(tile) == null)
                Notify(this);
        }
        
        public void HitEnemies()
        {
            var currentRoom = GetCurrentRoom();
            var playerPosition = currentRoom.GetPlayerPosition()!;

            var possiblePositions = new List<Position>
            {
                new Position { X = playerPosition.X + 1, Y = playerPosition.Y },
                new Position { X = playerPosition.X - 1, Y = playerPosition.Y },
                new Position { X = playerPosition.X, Y = playerPosition.Y - 1 },
                new Position { X = playerPosition.X, Y = playerPosition.Y + 1 }
            };

            foreach (var enemy in currentRoom.GetEnemies())
            {
                if (!possiblePositions.Contains(enemy.CurrentPosition)) continue;
                var newHealth = enemy.Hurt(1);
                if (newHealth <= 0) GetCurrentRoom().RemoveEnemy(enemy.CurrentPosition);
            }

            Notify(this);
        }

    }
}

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
        public event EventHandler<Game> Updated;
        private Player Player { get; set; }
        private List<Room> Rooms { get; set; } = new List<Room>();

        public ConsoleKey KeyPressed { get; private set; }
        public bool Quit { get; private set; } = false;

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
        public Entity UseEntity(Tile tile)
        {
            if (Player != tile.Player || tile.Entity == null) return null;

            var item = tile.Entity.Interact(Player, GetCurrentRoom());

            Notify(this);

            return item;
        }

        public bool UseConnection(Tile tile)
        {
            if (Player != tile.Player || tile.Connection == null) return false;

            var roomDirection = tile.Connection.Enter(GetCurrentRoom(), Player);

            if (!roomDirection.HasValue) return false;

            var (direction, room) = roomDirection.Value;

            GetCurrentRoom().RemovePlayer();

            room.SpawnPlayer(room.GetSpawnPosition(direction.GetOpposite()), Player);

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

        public Entity UseItem(Tile tile)
        {
            if (Player != tile.Player || tile.Entity == null) return null;
            var item = tile.Entity.Interact(Player, GetCurrentRoom());

            Notify(this);

            return item;
        }

        public void MovePlayer(Position newPosition)
        {
            var tile = Player.Move(GetCurrentRoom(), newPosition);
            if (tile == null || !UseConnection(tile) && UseItem(tile) == null) Notify(this);
        }
    }
}

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
            if (Player != tile.Player || tile.Connection == null) return false;

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
            if (Player != tile.Player || tile.Entity == null) return null;
            var entity = tile.Entity.Interact(Player, GetCurrentRoom());

            Notify(this);

            return entity;
        }

        public void MovePlayer(Position newPosition)
        {
            var tile = Player.Move(GetCurrentRoom(), newPosition);
            if (tile == null || !UseConnection(tile) && UseEntity(tile) == null) Notify(this);
        }
    }
}

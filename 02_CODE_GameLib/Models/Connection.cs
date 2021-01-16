#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models.Connectors;

namespace CODE_GameLib.Models
{
    public class Connection : IRenderable
    {
        public ConsoleColor Color
        {
            get => Door?.Color ?? ConsoleColor.White;
        }
        public Ladder? Ladder { get; set; }
        public Door? Door { get; set; }
        public Dictionary<Direction, Room> Connections { get; } = new Dictionary<Direction, Room>();
        public KeyValuePair<Direction, Room>? Enter(Room currentRoom, Player player)
        {
            if (Door != null && !Door.Enter(player)) return null;

            KeyValuePair<Direction, Room> connection = Connections.FirstOrDefault(c => c.Value != currentRoom);
            if (connection.Value == null) return null;

            currentRoom.RemovePlayer();
            connection.Value.SpawnPlayer(Ladder != null ? Ladder.GetCorrectPosition(connection.Key) : connection.Value.GetSpawnPosition(connection.Key.GetOpposite()), player);

            return connection;
        }

        public IRenderable? GetVisual()
        {
            if (Door != null) return Door;
            return Ladder;
       }
    }
}
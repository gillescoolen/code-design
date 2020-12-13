using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models.Doors;

namespace CODE_GameLib.Models
{
    public class Connection : IRenderable
    {
        private ConsoleColor _color = ConsoleColor.White;
        public ConsoleColor Color
        {
            get => Door?.Color ?? ConsoleColor.White;
            set => _color = value;
        }
        public Door Door { get; set; }
        public Dictionary<Side, Room> Connections { get; } = new Dictionary<Side, Room>();
        public KeyValuePair<Side, Room>? Enter(Room currentRoom, Player player)
        {
            if (Door != null && !Door.Enter(player)) return null;

            return Connections.FirstOrDefault(c => c.Value != currentRoom);
        }
    }
}
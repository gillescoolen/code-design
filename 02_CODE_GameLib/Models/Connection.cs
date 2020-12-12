using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models.Doors;

namespace CODE_GameLib.Models
{
    public class Connection : IRenderable
    {
        private string color = "white";
        public string Color
        {
            get => Door?.Color ?? color;
            set => color = value;
        }
        public Door Door { get; set; }
        public Dictionary<Side, Room> Connections { get; } = new Dictionary<Side, Room>();
        public KeyValuePair<Side, Room>? Enter(Room currentRoom, Player player)
        {
            if (Door != null && !Door.Interact(player))
            {
                return null;
            }

            return Connections.FirstOrDefault(c => c.Value != currentRoom);
        }
    }
}
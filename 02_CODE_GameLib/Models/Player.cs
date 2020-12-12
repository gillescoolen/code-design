using System.Collections.Generic;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Player
    {
        private List<Entity> Items;
        public int StartRoomId { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }

        public int Lives { get; set; }
    }
}
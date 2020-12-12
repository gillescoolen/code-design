using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Player : IRenderable
    {
        private List<Entity> Entities = new List<Entity>();
        public readonly Position StartPosition;
        public int StartRoomId { get; set; }
        public int Lives { get; set; }
        public string Color { get; set; } = "blue";

        public Player(int startRoomId, Position position, int lives)
        {
            StartRoomId = startRoomId;
            StartPosition = position;
            Lives = lives;
        }

        public IEnumerable<Entity> GetEntities<T>()
        {
            return Entities.Where(i => i is T)
                .ToList();
        }
    }
}
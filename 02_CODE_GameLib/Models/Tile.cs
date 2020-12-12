using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Tile
    {
        public Player Player { get; set; }
        public Entity Entity { get; set; }
        public Connection Connection { get; set; }
        public Position Position { get; set; }

        public IRenderable GetVisual()
        {
            if (Player != null)
                return Player;

            if (Entity != null)
                return Entity;

            return Connection;
        }
    }
}
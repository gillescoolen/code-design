using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Tile
    {
        public Player Player { get; set; }
        public Entity Entity { get; set; }
        public Connection Connection { get; set; }
        public Position Position { get; set; }

        public Tile(Position position)
        {
            Position = position;
        }
        
        public IRenderable GetVisual()
        {
            if (Player != null) return Player;

            return Entity ?? Connection?.GetVisual();
        }
    }
}
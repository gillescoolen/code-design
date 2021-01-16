using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public class Tile
    {
        public IActor? Actor { get; set; }
        public Entity Entity { get; set; }
        public Connection Connection { get; set; }
        public Position Position { get; set; }

        public Tile(Position position)
        {
            Position = position;
        }
        
        public IRenderable GetVisual()
        {
            if (Actor != null) return Actor;

            return Entity ?? Connection?.GetVisual();
        }
    }
}
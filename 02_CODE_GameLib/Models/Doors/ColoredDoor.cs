using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors
{
    public class ColoredDoor : Door
    {
        public override string Color { get; set; } = "white";

        public override bool Interact(Player player)
        {
            var keys = player.GetEntities<Key>();
            return keys.FirstOrDefault(k => k.Color == Color) != null;
        }
    }
}
using System;
using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors
{
    public class ColoredDoor : Door
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override bool Enter(Player player)
        {
            var keys = player.GetEntities<Key>();
            return keys.FirstOrDefault(k => k.Color == Color) != null;
        }
    }
}
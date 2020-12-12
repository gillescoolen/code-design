using System;
using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Doors
{
    public class ColoredDoor : Door
    {
        public override string Color { get; set; } = "white";

        public override bool Interact(Player player)
        {
            Console.WriteLine("");
            Console.WriteLine("Interacting with Colored Door");
            Console.WriteLine("");
            var keys = player.GetEntities<Key>();
            Console.WriteLine(keys);
            Console.WriteLine("");
            return keys.FirstOrDefault(k => k.Color == Color) != null;
        }
    }
}
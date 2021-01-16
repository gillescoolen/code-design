using System;
using System.Linq;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models.Connectors
{
    public class ColoredDoor : Door
    {
        public override bool Enter(Player player)
        {
            var keys = player.GetInventory<Key>();
            return keys.FirstOrDefault(k => k.Color == Color) != null;
        }
    }
}
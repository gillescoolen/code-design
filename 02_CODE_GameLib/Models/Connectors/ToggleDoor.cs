using System;

namespace CODE_GameLib.Models.Connectors
{
    public class ToggleDoor : Door
    {
        public override bool Enter(Player player)
        {
            return Open || player.CanOpenDoors;
        }
    }
}
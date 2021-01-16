using System;

namespace CODE_GameLib.Models.Connectors
{
    public class ClosingGateDoor : Door
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override bool Enter(Player player)
        {
            if (!Open && !player.CanOpenDoors)
            {
                return false;
            }

            Toggle();

            return true;
        }

        public override bool CanEnter(Player player)
        {
            return Open;
        }
    }
}
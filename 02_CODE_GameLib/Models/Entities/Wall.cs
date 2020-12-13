using System;

namespace CODE_GameLib.Models.Entities
{
    public class Wall : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.DarkYellow;

        public override Entity Interact(Player player, Room room)
        {
            return this;
        }

        public override bool IsInteractable()
        {
            return false;
        }
    }
}
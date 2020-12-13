using System;

namespace CODE_GameLib.Models.Entities
{
    public class Wall : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.DarkYellow;

        public override Entity Interact(Player player, Room room) => this;

        public override bool IsInteractable() => false;
    }
}
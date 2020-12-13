using System;

namespace CODE_GameLib.Models.Entities
{
    public class Boobietrap : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override Entity Interact(Player player, Room room)
        {
            player.Hurt(Damage);
            return this;
        }
    }
}
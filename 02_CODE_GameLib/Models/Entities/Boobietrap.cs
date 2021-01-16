using System;

namespace CODE_GameLib.Models.Entities
{
    public class Boobietrap : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override Entity Interact(IActor actor, Room room, Game game)
        {
            actor.Hurt(Damage);
            return this;
        }
    }
}
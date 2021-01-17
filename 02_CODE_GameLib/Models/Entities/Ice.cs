using System;
using CODE_GameLib.Adapters;
using CODE_GameLib.Models;

namespace CODE_GameLib.Models.Entities
{
    public class Ice : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override Entity Interact(IActor actor, Room room, Game game)
        {
            if (room == null) return null;

            var position = actor.CurrentPosition.GetPrevious(actor.PreviousPosition).GetStep();
            game.Move(position, actor is EnemyAdapter, false);

            return this;
        }
    }
}
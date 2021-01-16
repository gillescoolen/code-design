using System;

namespace CODE_GameLib.Models.Entities
{
    public class DisappearingBoobietrap : Boobietrap
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override Entity Interact(IActor actor, Room room, Game game)
        {
            if (room == null) return null;

            base.Interact(actor, room, game);
            var tile = room.GetTileByEntity(this);
            tile.Entity = null;

            return this;
        }
    }
}
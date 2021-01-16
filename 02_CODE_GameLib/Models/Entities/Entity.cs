using System;

namespace CODE_GameLib.Models.Entities
{
    public abstract class Entity : IRenderable
    {
        public abstract ConsoleColor Color { get; set; }
        public int Damage { get; set; }
        public virtual Entity Interact(IActor actor, Room room, Game game)
        {
            actor.AddToInventory(this);
            var tile = room.GetTileByEntity(this);
            tile.Entity = null;
            return this;
        }

        public virtual bool IsInteractable()
        {
            return true;
        }
    }
}
using System;

namespace CODE_GameLib.Models.Entities
{
    public abstract class Entity : IRenderable
    {
        public abstract ConsoleColor Color { get; set; }
        public int Damage { get; set; }
        public virtual Entity Interact(Player player, Room room)
        {
            if (room == null) return null;

            player.AddToInventory(this);
            var tile = room.GetTileByItem(this);
            tile.Entity = null;

            return this;
        }

        public virtual bool IsInteractable()
        {
            return true;
        }
    }
}
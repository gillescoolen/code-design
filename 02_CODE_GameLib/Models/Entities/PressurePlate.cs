using System;
using CODE_GameLib.Models.Connectors;

namespace CODE_GameLib.Models.Entities
{
    public class PressurePlate : Entity
    {
        public override ConsoleColor Color { get; set; } = ConsoleColor.White;

        public override Entity Interact(Player player, Room room)
        {
            if (room == null) return null;

            var tiles = room.GetTilesByDoor<ToggleDoor>();

            tiles.ForEach(tile => { tile.Connection.Door.Toggle(); });

            return this;
        }
    }
}
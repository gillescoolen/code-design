namespace CODE_GameLib.Models.Entities
{
    public class DisappearingBoobietrap : Boobietrap
    {
        public override string Color { get; set; } = "white";

        public override Entity Interact(Player player, Room room)
        {
            if (room == null) return null;

            base.Interact(player, room);
            var tile = room.GetTileByItem(this);
            tile.Entity = null;

            return this;
        }
    }
}
namespace CODE_GameLib.Models.Entities
{
    public class Trap : Entity
    {
        public override string Color { get; set; } = "white";

        public override Entity Interact(Player player, Room room)
        {
            return this;
        }
    }
}
namespace CODE_GameLib.Models.Entities
{
    public class DisappearingTrap : Trap
    {
        public override string Color { get; set; } = "white";

        public override Entity Interact(Player player, Room room)
        {
            return this;
        }
    }
}
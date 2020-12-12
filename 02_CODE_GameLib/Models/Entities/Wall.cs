namespace CODE_GameLib.Models.Entities
{
    public class Wall : Entity
    {
        public override string Color { get; set; } = "yellow";

        public override Entity Interact(Player player, Room room)
        {
            return this;
        }

        public override bool IsInteractable()
        {
            return false;
        }
    }
}
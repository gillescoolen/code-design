namespace CODE_GameLib.Models.Entities
{
    public class Boobietrap : Entity
    {
        public override string Color { get; set; } = "white";

        public override Entity Interact(Player player, Room room)
        {
            player.Hurt(Damage);
            return this;
        }
    }
}
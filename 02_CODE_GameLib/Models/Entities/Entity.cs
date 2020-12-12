namespace CODE_GameLib.Models.Entities
{
    public abstract class Entity : IRenderable
    {
        public abstract string Color { get; set; }
        public int Damage { get; set; }
        public virtual Entity Interact(Player player, Room room)
        {
            return this;
        }

        public virtual bool IsInteractable()
        {
            return true;
        }
    }
}
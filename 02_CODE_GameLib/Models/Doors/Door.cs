namespace CODE_GameLib.Models.Doors
{
    public abstract class Door : IRenderable
    {
        public virtual string Color { get; set; }
        protected bool Open { get; set; } = true;

        public virtual bool Interact(Player player)
        {
            return true;
        }

        public virtual void Toggle()
        {
            Open = !Open;
        }

        public virtual bool IsInteractable(Player player)
        {
            return IsInteractable(player);
        }
    }
}
using System;

namespace CODE_GameLib.Models.Connectors
{
    public abstract class Door : IRenderable
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
        protected bool Open { get; set; } = true;

        public virtual void Toggle()
        {
            Open = !Open;
        }

        public virtual bool Enter(Player player)
        {
            return true;
        }

        public virtual bool CanEnter(Player player)
        {
            return Enter(player);
        }
    }
}
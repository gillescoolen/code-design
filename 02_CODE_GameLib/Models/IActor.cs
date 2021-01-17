using System.Collections.Generic;
using CODE_GameLib.Models.Entities;

namespace CODE_GameLib.Models
{
    public interface IActor: IRenderable
    {
        public List<Entity> Inventory { get; }
        public int Lives { get; }
        public Tile Move(Room room, Position movePosition);
        public int Hurt(int damage);
        public List<Entity> GetInventory<T>();
        public void AddToInventory(Entity entity);
        public Position CurrentPosition { get; }
        public Position PreviousPosition { get; }
    }
}
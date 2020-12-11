public abstract class Entity : IPosition
{
    public int x { get; set; }
    public int y { get; set; }
    public void Activate(Player player) {
        // add entity to player
    }
}
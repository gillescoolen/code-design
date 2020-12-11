using System.Collections.Generic;
public class Player
{
    private List<Entity> items;
    public int startRoomId { get; set; }
    public int startX { get; set; }
    public int startY { get; set; }
    
    public int lives { get; set; }
}
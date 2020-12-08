using System.Collections.Generic;

public class Room
{

    public int Id { get; set; }
    public string Type { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Item> items { get; set; }
}
using System.Collections.Generic;
namespace CODE_GameLib.Models
{
    public class Room
    {

        public int Id { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Item> Items { get; set; }
    }
}
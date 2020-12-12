using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Models;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        private Player Player { get; set; }
        private List<Room> Rooms { get; set; } = new List<Room>();

        public ConsoleKey KeyPressed { get; private set; }
        public bool Quit { get; private set; } = false;

        public Game(Player player, List<Room> rooms)
        {
            Player = player;
            Rooms = rooms;
        }

        public void Run()
        {
            KeyPressed = Console.ReadKey().Key;
            Quit = KeyPressed == ConsoleKey.Escape;

            while (!Quit)
            {
                Updated?.Invoke(this, this);

                KeyPressed = Console.ReadKey().Key;
                Quit = KeyPressed == ConsoleKey.Escape;
            }

            Updated?.Invoke(this, this);
        }

        public Room GetCurrentRoom()
        {
            return Rooms.Find(r => r.GetPlayerPosition() != null)!;
        }

        public Room GetRoomById(int id)
        {
            return Rooms.FirstOrDefault(r => r.Id == id);
        }
    }
}

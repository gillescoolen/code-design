using System;
using System.Collections.Generic;
using System.Linq;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Entities;

namespace CODE_Frontend.Views
{
    public class GameView : View<GameController>
    {
        private readonly Dictionary<Entity, char> entityCharacters = new Dictionary<Entity, char>
        {
            { new Wall(), '#' },
            { new SankaraStone(), 'S' },
            { new Boobietrap(), 'O' },
            { new Key(), 'K' },
            { new PressurePlate(), 'T' },
            { new DisappearingBoobietrap(), '@' }
        };

        private readonly Dictionary<Door, char> doorCharacters = new Dictionary<Door, char>
        {
            { new ClosingGateDoor(), 'u' },
            { new ToggleDoor(), '┴' }
        };

        public GameView(GameController controller) : base(controller,
            new Input((int)ConsoleKey.LeftArrow, () => controller.MovePlayer(Side.WEST)),
            new Input((int)ConsoleKey.UpArrow, () => controller.MovePlayer(Side.NORTH)),
            new Input((int)ConsoleKey.RightArrow, () => controller.MovePlayer(Side.EAST)),
            new Input((int)ConsoleKey.DownArrow, () => controller.MovePlayer(Side.SOUTH)))
        {
        }

        public override void Draw()
        {
            Console.WriteLine("Welcome to the Temple of Doom! \n\n");
            foreach (var (position, tile) in Controller.GetTiles())
            {
                var character = GetCharacter(tile);

                Console.ForegroundColor = tile.GetVisual()?.Color ?? ConsoleColor.White;
                Console.Write(character);
                Console.ResetColor();

                if (position.X == Controller.GetCurrentRoom().Width - 1)
                {
                    Console.Write("\n");
                }
            }
            Console.Write("\nLives: ");
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < Controller.GetLives(); i++)
            {
                Console.Write("<3 ");
            }
            Console.ResetColor();
        }
        private char GetCharacter(Tile tile)
        {
            if (tile.Entity != null)
            {
                return entityCharacters.FirstOrDefault(character => character.Key.GetType() == tile.Entity.GetType()).Value;
            }

            if (tile.Connection?.Door == null) return tile.Player != null ? 'X' : ' ';
            var (key, value) = doorCharacters.FirstOrDefault(character => character.Key.GetType() == tile.Connection.Door.GetType());

            if (key != null)
            {
                return value;
            }

            return tile.Position.X == Controller.GetCurrentRoom().Width - 1 || tile.Position.X == 0 ? '|' : '=';

        }
    }
}
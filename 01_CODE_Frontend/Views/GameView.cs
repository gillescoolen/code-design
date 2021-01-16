using System;
using System.Collections.Generic;
using System.Linq;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;
using CODE_GameLib.Adapters;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Connectors;
using CODE_GameLib.Models.Entities;

namespace CODE_Frontend.Views
{
    public class GameView : View<GameController>
    {
        private readonly Dictionary<Type, char> characters = new Dictionary<Type, char>
        {
            { typeof(Wall), '#' },
            { typeof(SankaraStone), 'S' },
            { typeof(Boobietrap), 'O' },
            { typeof(Key), 'K' },
            { typeof(PressurePlate), 'T' },
            { typeof(DisappearingBoobietrap), '@' },
            { typeof(Ladder), 'L' },
            { typeof(Player), 'X' },
            { typeof(ClosingGateDoor), 'u' },
            { typeof(ToggleDoor), '┴' },
            { typeof(EnemyAdapter), 'E' }
        };

        public GameView(GameController controller) : base(controller,
            new Input((int)ConsoleKey.LeftArrow, () => controller.Move(Direction.WEST)),
            new Input((int)ConsoleKey.UpArrow, () => controller.Move(Direction.NORTH)),
            new Input((int)ConsoleKey.RightArrow, () => controller.Move(Direction.EAST)),
            new Input((int)ConsoleKey.DownArrow, () => controller.Move(Direction.SOUTH)),
            new Input((int)ConsoleKey.Spacebar, () => controller.HitEnemies()),
            new Input((int) ConsoleKey.L,() => controller.ToggleDieCheat()),
            new Input((int)ConsoleKey.D, () => controller.ToggleDoorCheat())
        )
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

            for (int i = 0; i < Controller.GetLives(); i++) Console.Write("<3 ");

            Console.ResetColor();
        }
        private char GetCharacter(Tile tile)
        {
            var visual = tile.GetVisual();
            if (visual == null) return ' ';

            var character = characters.FirstOrDefault(character => character.Key == visual.GetType()).Value;

            if (character == char.MinValue)
                return tile.Position.X == Controller.GetCurrentRoom().Width - 1 || tile.Position.X == 0 ? '|' : '=';

            return character;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Doors;
using CODE_GameLib.Models.Entities;
using Pastel;

namespace CODE_Frontend.Views
{
    public class GameView : View<GameController>
    {
        private readonly Dictionary<Entity, char> itemSymbols = new Dictionary<Entity, char>
        {
            { new Wall(), '#' },
            { new SankaraStone(), 'S' },
            { new Boobietrap(), 'O' },
            { new Key(), 'K' },
            { new PressurePlate(), 'T' },
            { new DisappearingBoobietrap(), '@' }
        };

        private readonly Dictionary<Door, char> doorSymbols = new Dictionary<Door, char>
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

        public override void Draw(StringBuilder builder)
        {
            builder.Append("Welcome to the Temple of Doom!");
            builder.AppendLine();
            builder.AppendLine();

            foreach (var (position, tile) in Controller.GetTiles())
            {
                var symbol = GetSymbol(tile);

                builder.Append(
                    $"{symbol.ToString().Pastel($"{Color.FromName(tile.GetVisual()?.Color ?? "white").ToArgb():x6}")} ");

                if (position.X == Controller.GetCurrentRoom().Width - 1)
                {
                    builder.AppendLine();
                }
            }

            builder.AppendLine();
            builder.Append($"Lives: {Controller.GetLives()}");
        }
        private char GetSymbol(Tile tile)
        {
            if (tile.Entity != null)
            {
                return itemSymbols.FirstOrDefault(symbol => symbol.Key.GetType() == tile.Entity.GetType()).Value;
            }

            if (tile.Connection?.Door == null) return tile.Player != null ? 'X' : ' ';
            var (key, value) = doorSymbols.FirstOrDefault(symbol => symbol.Key.GetType() == tile.Connection.Door.GetType());

            if (key != null)
            {
                return value;
            }

            return tile.Position.X == Controller.GetCurrentRoom().Width - 1 || tile.Position.X == 0 ? '|' : '=';

        }
    }
}
#nullable enable
using System.Collections.Generic;
using CODE_Frontend.Views;
using CODE_GameLib.Models;
using CODE_GameLib.Models.Entities;

namespace CODE_Frontend.Controllers
{
    public class GameController : Controller<GameController>
    {
        public GameController(Program program) : base(program)
        {
        }

        public override View<GameController> CreateView()
        {
            return new GameView(this);
        }

        public override void Update()
        {
            if (Program.Game.IsOver() || Program.Game.HasBeenWon())
            {
                Program.OpenController<EndController>();
            }
        }

        public void Move(Direction direction)
        {
            var game = Program.Game;
            game.Move(direction.GetStep());
        }

        public Dictionary<Position, Tile> GetTiles()
        {
            return GetCurrentRoom().Tiles;
        }

        public Room GetCurrentRoom()
        {
            return Program.Game.GetCurrentRoom();
        }

        public int GetLives()
        {
            return Program.Game.GetPlayerLives();
        }
        public void HitEnemies()
        {
            Program.Game.HitEnemies();
        }
        public void ToggleDoorCheat()
        {
            Program.Game.ToggleDoorCheat();
        }

        public void ToggleImmortality()
        {
            Program.Game.ToggleImmortality();
        }
    }
}
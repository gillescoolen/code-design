using System;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;

namespace CODE_Frontend.Views
{
    public class EndView : View<EndController>
    {
        public EndView(EndController controller) : base(controller, new Input((int)ConsoleKey.Escape, controller.Quit))
        {
        }

        public override void Draw()
        {
            Console.WriteLine(Controller.GameHasBeenWon() ? "Every Sankara Stone has been found. Congratulations!" : "Oh no, you died!");
            Console.WriteLine("\nPress the escape key to quit this game...");
        }
    }
}
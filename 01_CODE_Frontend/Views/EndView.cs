using System;
using System.Text;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;

namespace CODE_Frontend.Views
{
    public class EndView : View<EndController>
    {
        public EndView(EndController controller) : base(controller, new Input((int)ConsoleKey.Escape, controller.Quit))
        {
        }

        public override void Draw(StringBuilder builder)
        {
            builder.AppendLine(Controller.GameHasBeenWon() ? "Every Sankara Stone has been found. Congratulations!" : "Oh no, you died!");

            builder.AppendLine();
            builder.AppendLine("Press the escape key to quit this game...");
        }
    }
}
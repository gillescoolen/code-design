using System;
using System.Text;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;

namespace CODE_Frontend.Views
{
    public class MainView : View<MainController>
    {
        public MainView(MainController controller) : base(controller, new Input((int)ConsoleKey.Enter, controller.Start))
        {
        }
        public override void Draw()
        {
            Console.WriteLine("Press enter to play!");
        }
    }
}
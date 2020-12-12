using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public class EndController : Controller<EndController>
    {
        public EndController(Program program) : base(program)
        {
        }

        public override View<EndController> CreateView()
        {
            return new EndView(this);
        }

        public override void Update()
        {
        }

        public bool GameHasBeenWon()
        {
            return Program.Game.HasBeenWon();
        }

        public void Quit()
        {
            Program.Exit();
        }
    }
}
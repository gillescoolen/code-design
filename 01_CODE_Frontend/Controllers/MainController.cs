using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public class MainController : Controller<MainController>
    {
        public MainController(Program program) : base(program)
        {
        }

        public override View<MainController> CreateView()
        {
            return new MainView(this);
        }

        public override void Update()
        {
            
        }

        public void Start()
        {
            Program.OpenController<GameController>();
        }
    }
}
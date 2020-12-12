using CODE_Frontend.Views;

namespace CODE_Frontend.Controllers
{
    public abstract class Controller
    {
        protected readonly Program Program;

        internal Controller(Program program)
        {
            Program = program;
        }

        public abstract void Update();
    }

    public abstract class Controller<T> : Controller where T : Controller<T>
    {
        protected Controller(Program program) : base(program)
        {
        }

        public abstract View<T> CreateView();
    }
}
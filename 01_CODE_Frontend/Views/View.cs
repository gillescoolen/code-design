using System.Collections.Generic;
using System.Text;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;

namespace CODE_Frontend.Views
{
    public abstract class View
    {
        private readonly Input[] inputs;

        public IEnumerable<Input> Inputs => (Input[]) inputs.Clone();

        internal View(Input[] inputs)
        {
            this.inputs = inputs;
        }

        public abstract void Draw(StringBuilder builder);
    }

    public abstract class View<T> : View where T : Controller<T>
    {
        protected readonly T Controller;

        protected View(T controller, params Input[] inputs) : base(inputs)
        {
            Controller = controller;
        }
    }
}
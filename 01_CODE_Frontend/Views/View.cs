using System.Collections.Generic;
using System.Text;
using CODE_Frontend.Controllers;
using CODE_Frontend.Models;

namespace CODE_Frontend.Views
{
    public abstract class View
    {
        private readonly Input[] _Inputs;

        public IEnumerable<Input> Inputs => (Input[])_Inputs.Clone();

        internal View(Input[] inputs)
        {
            this._Inputs = inputs;
        }

        public abstract void Draw();
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
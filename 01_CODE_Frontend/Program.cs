using System;
using System.Text;
using CODE_FileSystem;
using CODE_Frontend.Controllers;
using CODE_Frontend.Views;
using CODE_GameLib;

namespace CODE_Frontend
{
    public class Program : IObserver<Game>
    {
        public Game Game;

        private IDisposable UnSubscriber;
        private Controller Controller;
        private View View;
        private bool IsRunning = true;

        private Program()
        {
            Console.Title = "Temple of Doom";
            Console.OutputEncoding = Encoding.UTF8;

            var gameReader = new GameReader();
            Game = gameReader.Read("./Levels/TempleOfDoom.json");

            OpenController<MainController>();
            UnSubscriber = Game.Subscribe(this);

            Input();
        }

        public void Exit()
        {
            OnCompleted();
            IsRunning = false;
            Console.Clear();
            Environment.Exit(1);
        }

        public static void Main(string[] args)
        {
            new Program();
        }

        public void OpenController<T>() where T : Controller<T>
        {
            var controller = (T)Activator.CreateInstance(typeof(T), this);

            Controller = controller;
            View = controller?.CreateView();
            OnNext(Game);
        }

        private void Input()
        {
            while (IsRunning)
            {
                var key = Console.ReadKey(true);

                foreach (var input in View.Inputs)
                {
                    if (input.Character != (int)key.Key) continue;

                    input.Action();
                }
            }
        }

        public void OnCompleted()
        {
            UnSubscriber.Dispose();
        }

        public void OnError(Exception exception)
        {
            Console.WriteLine($"Uh oh, something went wrong!");
            Console.WriteLine(exception.Message);
        }

        public void OnNext(Game value)
        {
            Game = value;

            Console.Clear();

            Controller.Update();

            View.Draw();

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
        }
    }
}
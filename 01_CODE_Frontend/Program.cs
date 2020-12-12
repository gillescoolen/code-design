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

        private IDisposable unSubscriber;
        private Controller controller;
        private View view;
        private bool running = true;

        private Program()
        {
            Console.Title = "Temple of Doom";
            Console.OutputEncoding = Encoding.UTF8;

            var gameReader = new GameReader();
            Game = gameReader.Read("./Levels/TempleOfDoom.json");

            OpenController<MainController>();
            unSubscriber = Game.Subscribe(this);

            Input();
        }

        public void Exit()
        {
            OnCompleted();
            running = false;
            Environment.Exit(1);
        }

        public static void Main(string[] args)
        {
            new Program();
        }

        public void OpenController<T>() where T : Controller<T>
        {
            var newController = (T)Activator.CreateInstance(typeof(T), this);

            controller = newController;
            view = newController?.CreateView();
            OnNext(Game);
        }

        private void Input()
        {
            while (running)
            {
                var key = Console.ReadKey(true);

                foreach (var input in view.Inputs)
                {
                    if (input.Character != (int)key.Key) continue;

                    input.Action();
                }
            }
        }

        public void OnCompleted()
        {
            unSubscriber.Dispose();
        }

        public void OnError(Exception error)
        {
            Console.Write($"Exception!: {error.Message}");
        }

        public void OnNext(Game value)
        {
            Game = value;

            Console.Clear();

            controller.Update();
            var builder = new StringBuilder();
            view.Draw(builder);

            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            Console.Write(builder.ToString());
        }
    }
}
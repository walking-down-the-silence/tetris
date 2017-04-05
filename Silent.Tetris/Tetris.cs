using System;
using System.Threading.Tasks;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Renderers;
using Silent.Tetris.Views;

namespace Silent.Tetris
{
    internal class Tetris
    {
        private static void Main()
        {
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedException;

            IContainer gameServiceLocator = BuildServiceLocator();

            IObserveAsync<ICommand> commandObserver = gameServiceLocator.Resolve<IObserveAsync<ICommand>>();
            IDisposable commandObserverDisposable = commandObserver.ObserveAsync();

            IConfiguration gameConfiguration = gameServiceLocator.Resolve<IConfiguration>();
            IView homeView = new HomeView(gameServiceLocator);
            Initialize(gameConfiguration, homeView);

            INavigationService navigationService = gameServiceLocator.Resolve<INavigationService>();
            navigationService.Navigate(homeView);

            using (commandObserverDisposable)
            {
                while (navigationService.CurrentView != null)
                {
                    Initialize(gameConfiguration, navigationService.CurrentView);
                    navigationService.CurrentView.Render();
                    Task.Delay(50).Wait();
                }
            }
        }

        private static IContainer BuildServiceLocator()
        {
            IContainer gameContainer = new ServiceLocator();
            gameContainer.Register<IConfiguration>(BuildConsoleConfiguration("Tetris"));
            gameContainer.Register<INavigationService>(new NavigationService());
            gameContainer.Register<ISpriteRenderer>(new SpriteRenderer());
            gameContainer.Register<IFactory<IFigure>>(new FigureFactory());
            gameContainer.Register<IRandomGenerator<IFigure>>(container => new FigureRandomGenerator(container.Resolve<IFactory<IFigure>>()));
            gameContainer.Register<IRepository<Player>>(new JsonRepository("highscores.json"));
            gameContainer.Register<IObserveAsync<ICommand>>(new ConsoleCommandsObserveAsync());
            return gameContainer;
        }

        private static void TaskSchedulerOnUnobservedException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            eventArgs.SetObserved();
            Console.WriteLine(eventArgs.Exception.Flatten());
        }

        private static void Initialize(IConfiguration configuration, IView currentView)
        {
            if(currentView.Size.Width > 0 && currentView.Size.Height > 0)
            {
                if (currentView.Size.Width * 2 != Console.WindowWidth || currentView.Size.Height != Console.WindowHeight)
                {
                    Console.SetWindowSize(currentView.Size.Width * 2, currentView.Size.Height);
                    Console.SetBufferSize(currentView.Size.Width * 2, currentView.Size.Height);
                }
            }

            Console.CursorVisible = false;
            Console.Title = configuration.Title;
        }

        private static IConfiguration BuildConsoleConfiguration(string title)
        {
            Size gameFieldDefaultSize = new Size(10, 22);
            return new GameConsoleConfiguration(new Position(10, 10), gameFieldDefaultSize, title);
        }

        private class GameConsoleConfiguration : IConfiguration
        {
            public GameConsoleConfiguration(Position position, Size gameFieldsize, string title)
            {
                Position = position;
                GameFieldSize = gameFieldsize;
                Title = title;
            }

            public Position Position { get; }

            public Size GameFieldSize { get; }

            public string Title { get; }
        }
    }
}

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
            IConfiguration gameConfiguration = gameServiceLocator.Resolve<IConfiguration>();
            IView homeView = new HomeView(gameServiceLocator);
            Initialize(gameConfiguration, homeView);

            INavigationService navigationService = gameServiceLocator.Resolve<INavigationService>();
            navigationService.Navigate(homeView);

            while (navigationService.CurrentView != null)
            {
                Initialize(gameConfiguration, navigationService.CurrentView);
                navigationService.CurrentView.Render();
                Task.Delay(100).Wait();
            }
        }

        private static IContainer BuildServiceLocator()
        {
            IContainer gameContainer = new ServiceLocator();
            gameContainer.Register<IConfiguration>(BuildConsoleConfiguration("Tetris"));
            gameContainer.Register<INavigationService>(new NavigationService());
            gameContainer.Register<ISpriteRenderable>(container => new SpriteRenderer());
            gameContainer.Register<IFactory<IFigure>>(container => new FigureFactory());
            gameContainer.Register<IRandomGenerator<IFigure>>(container => new FigureRandomGenerator(container.Resolve<IFactory<IFigure>>()));
            gameContainer.Register<IRepository<Player>>(container => new JsonRepository("highscores.json"));
            return gameContainer;
        }

        private static void TaskSchedulerOnUnobservedException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            eventArgs.SetObserved();
            Console.WriteLine(eventArgs.Exception.Flatten());
        }

        private static void Initialize(IConfiguration configuration, IView currentView)
        {
            if (currentView.Size.Width * 2 != Console.WindowWidth || currentView.Size.Height != Console.WindowHeight)
            {
                Console.SetWindowSize(currentView.Size.Width * 2, currentView.Size.Height);
                Console.SetBufferSize(currentView.Size.Width * 2, currentView.Size.Height);
            }

            Console.CursorVisible = false;
            Console.Title = configuration.Title;
        }

        private static IConfiguration BuildConsoleConfiguration(string title)
        {
            return new GameConsoleConfiguration(new Position(10, 10), new Size(12, 24), title);
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

using System;
using System.Threading.Tasks;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Rendering;
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
            Initialize(gameConfiguration);

            INavigationService navigationService = gameServiceLocator.Resolve<INavigationService>();
            navigationService.Navigate(new HomeView(gameConfiguration.Size, gameServiceLocator));

            while (navigationService.CurrentView != null)
            {
                navigationService.CurrentView.Render();
                Task.Delay(100).Wait();
            }
        }

        private static IContainer BuildServiceLocator()
        {
            IContainer gameContainer = new GameIocContainer();
            gameContainer.Register<IConfiguration>(BuildConsoleConfiguration("Tetris"));
            gameContainer.Register<INavigationService>(new NavigationService());
            gameContainer.Register<ISpriteRenderable>(container => new SpriteRenderer());
            gameContainer.Register<IFactory<IFigure>>(container => new FigureFactory());
            gameContainer.Register<IRandomGenerator<IFigure>>(container => new FigureRandomGenerator(container.Resolve<IFactory<IFigure>>()));
            gameContainer.Register<ICommandBus>(new CommandBus());

            return gameContainer;
        }

        private static void TaskSchedulerOnUnobservedException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            eventArgs.SetObserved();
            Console.WriteLine(eventArgs.Exception.Flatten());
        }

        private static void Initialize(IConfiguration configuration)
        {
            Console.SetWindowSize(configuration.Size.Width * 2, configuration.Size.Height);
            Console.SetBufferSize(configuration.Size.Width * 2, configuration.Size.Height);
            Console.CursorVisible = false;
            Console.Title = configuration.Title;
        }

        private static IConfiguration BuildConsoleConfiguration(string title)
        {
            return new GameConsoleConfiguration(new Position(10, 10), new Size(50, 60), title);
        }

        private class GameConsoleConfiguration : IConfiguration
        {
            public GameConsoleConfiguration(Position position, Size size, string title)
            {
                Position = position;
                Size = size;
                Title = title;
            }

            public Position Position { get; }

            public Size Size { get; }

            public string Title { get; }
        }
    }
}

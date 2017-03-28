using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;
using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private readonly IContainer _container;
        private readonly IGameView _gameView;
        private IGameEngine _gameEngine;
        private ICommandBus _commandBus;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _gameEngineDisposable;
        private IDisposable _commandObserverDisposable;

        public GamePresenter(GameView gameView, IContainer container)
        {
            _gameView = gameView;
            _container = container;
        }

        public IGameField Field => _gameEngine.Field;

        public IGameState State => _gameEngine.State;

        public void Initialize()
        {
            _commandBus = _container.Resolve<ICommandBus>();
            _gameEngine = new GameEngine(_container, _commandBus);
            _gameEngineDisposable = _gameEngine.Run();

            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Escape:
                    _commandBus.Complete();
                    _commandBus.Dispose();
                    _gameEngineDisposable.Dispose();
                    _commandObserverDisposable.Dispose();
                    _gameView.NavigationService.Navigate(new HomeView(_gameView.Size, _container));
                    break;
                default:
                    _commandBus.Add(command);
                    break;
            }
        }
    }
}
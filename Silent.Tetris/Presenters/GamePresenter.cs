using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private readonly IContainer _container;
        private INavigationService _navigationService;
        private IGameEngine _gameEngine;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _gameEngineDisposable;
        private IDisposable _commandObserverDisposable;

        public GamePresenter(IContainer container)
        {
            _container = container;
        }

        public IGameField Field => _gameEngine.Field;

        public IGameState State => _gameEngine.State;

        public void Initialize()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();

            _gameEngine = new GameEngine(_container);
            _gameEngine.StateChanged += HandleStateChanged;
            _gameEngineDisposable = _gameEngine.Run();
        }

        private void CheckGameOver()
        {
            if (_gameEngine.IsGameOver())
            {
                _gameEngineDisposable.Dispose();
                _commandObserverDisposable.Dispose();

                var playerScoresRepository = _container.Resolve<IRepository<Player>>();
                playerScoresRepository.Load();
                playerScoresRepository.Add(new Player("Unknown", _gameEngine.State.CurrentScore));
                playerScoresRepository.Save();

                _navigationService.Navigate(new GameOverView(_container, _gameEngine.State.CurrentScore));
            }
        }

        private void HandleStateChanged(object sender, GameStateEventArgs e)
        {
            CheckGameOver();
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Escape:
                    _gameEngineDisposable.Dispose();
                    _commandObserverDisposable.Dispose();
                    _navigationService.Navigate(new HomeView(_container));
                    break;
                case ConsoleKey.Enter:
                    CheckGameOver();
                    break;
                case ConsoleKey.LeftArrow:
                    _gameEngine.MoveCurrentFigure(MotionDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                    _gameEngine.MoveCurrentFigure(MotionDirection.Right);
                    break;
                case ConsoleKey.DownArrow:
                    _gameEngine.MoveCurrentFigure(MotionDirection.Down);
                    break;
                case ConsoleKey.Spacebar:
                    _gameEngine.RotateCurrentFigure();
                    break;
            }
        }
    }
}
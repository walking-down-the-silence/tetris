using System;
using Silent.Practices.DDD;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private readonly IDependencyResolver _container;
        private INavigationService _navigationService;
        private IRepository<GameField, Guid> _repository;
        private IGameEngine _gameEngine;
        private IObserveAsync<ICommand> _commandObserver;
        private IDisposable _gameEngineDisposable;

        public GamePresenter(IDependencyResolver container)
        {
            _container = container;
        }

        public IGameField Field => _repository.GetById(Guid.Empty);
        
        public void Initialize()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _repository = _container.Resolve<IRepository<GameField, Guid>>();
            _commandObserver = _container.Resolve<IObserveAsync<ICommand>>();
            _commandObserver.Update += Handle;

            _gameEngine = _container.Resolve<IGameEngine>();
            _gameEngine.StateChanged += HandleStateChanged;
            _gameEngineDisposable = _gameEngine.Run(Guid.Empty);
        }

        private void CheckGameOver()
        {
            if (_gameEngine.IsGameOver())
            {
                _gameEngineDisposable.Dispose();
                _commandObserver.Update -= Handle;

                // TODO: fix the high score saving
                //var playerScoresRepository = _container.Resolve<IRepository<ScoreRecord>>();
                //playerScoresRepository.Load();
                //playerScoresRepository.Add(new ScoreRecord(Guid.Empty, "Unknown", 0));
                //playerScoresRepository.Save();

                _navigationService.Navigate(new GameOverView(_container, 0));
            }
        }

        private void HandleStateChanged(object sender, GameStateEventArgs e)
        {
            CheckGameOver();
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;
            IGameField gameField = _repository.GetById(Guid.Empty);

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Escape:
                    _gameEngineDisposable.Dispose();
                    _commandObserver.Update -= Handle;
                    _navigationService.Navigate(new HomeView(_container));
                    break;
                case ConsoleKey.Enter:
                    CheckGameOver();
                    break;
                case ConsoleKey.LeftArrow:
                    gameField.MoveCurrentFigure(MotionDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                    gameField.MoveCurrentFigure(MotionDirection.Right);
                    break;
                case ConsoleKey.DownArrow:
                    gameField.MoveCurrentFigure(MotionDirection.Down);
                    break;
                case ConsoleKey.Spacebar:
                    gameField.RotateCurrentFigure();
                    break;
            }
        }
    }
}
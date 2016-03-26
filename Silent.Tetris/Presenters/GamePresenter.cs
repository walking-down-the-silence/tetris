using System;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core;
using Silent.Tetris.Observers;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private readonly GameView _gameView;
        private IGameField _gameField;
        private GameEngine _gameEngine;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _gameEngineDisposable;
        private IDisposable _commandObserverDisposable;

        public GamePresenter(GameView gameView)
        {
            _gameView = gameView;
        }

        public void Initialize()
        {
            _gameField = new GameField(new Size(Console.WindowWidth, Console.WindowHeight));

            _gameEngine = new GameEngine();
            _gameEngineDisposable = _gameEngine.Run(_gameField);

            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        public IGameField GameField => _gameField;

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.LeftArrow:
                    GameField.MoveCurrentFigure(MotionDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                    GameField.MoveCurrentFigure(MotionDirection.Right);
                    break;
                case ConsoleKey.DownArrow:
                    GameField.MoveCurrentFigure(MotionDirection.Down);
                    break;
                case ConsoleKey.Spacebar:
                    GameField.RotateCurrentFigure();
                    break;
                case ConsoleKey.Escape:
                    _gameEngineDisposable.Dispose();
                    _commandObserverDisposable.Dispose();
                    _gameView.NavigationService.Navigate(new HomeView());
                    break;
            }
        }
    }
}
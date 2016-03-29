using System;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;
using Silent.Tetris.Observers;

namespace Silent.Tetris.Core
{
    public class GameEngine : IGameEngine
    {
        private IGameField _gameField;
        private IObserveAsync<ICommand> _gameCommandObserveAsync;

        public IGameField GameField => _gameField;

        public IDisposable Run(IGameField gameField)
        {
            _gameField = gameField;
            _gameCommandObserveAsync = new GameEngineCommandsObserveAsync();
            _gameCommandObserveAsync.Update += GameCommandObserveAsyncOnUpdate;
            return _gameCommandObserveAsync.ObserveAsync();
        }

        private void GameCommandObserveAsyncOnUpdate(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand) command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.DownArrow:
                    _gameField.MoveCurrentFigure(MotionDirection.Down);
                    break;
            }
        }
    }
}

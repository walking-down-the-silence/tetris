using System;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core;
using Silent.Tetris.Core.Panels;
using Silent.Tetris.Observers;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class GamePresenter : IGamePresenter
    {
        private const int LayoutMargin = 1;
        private const int LeftPanelWidth = 0;
        private const int RightPanelWidth = 20;
        private readonly GameView _gameView;
        private IGameField _gameField;
        private IRightInfoField _rightInfoField;
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
            InitializeGameField();
            InitializeRightInfoField();

            _gameEngine = new GameEngine();
            _gameEngineDisposable = _gameEngine.Run(_gameField);

            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        private void InitializeRightInfoField()
        {
            int rightFieldLeft = _gameView.Size.Width - RightPanelWidth - 1;
            int rightFieldWidth = RightPanelWidth;
            Position rightFieldPosition = new Position(rightFieldLeft, 0);
            Size rightFieldSize = new Size(rightFieldWidth, _gameView.Size.Height);
            _rightInfoField = new RightInfoField(rightFieldPosition, rightFieldSize);
        }

        private void InitializeGameField()
        {
            int gameFieldLeft = LeftPanelWidth + LayoutMargin;
            int gameFieldWidth = _gameView.Size.Width - RightPanelWidth - LeftPanelWidth - LayoutMargin * 2;
            Position gameFieldPosition = new Position(gameFieldLeft, 0);
            Size gameFieldSize = new Size(gameFieldWidth, _gameView.Size.Height);
            _gameField = new GameField(gameFieldPosition, gameFieldSize);
        }

        public IField GameField => _gameField;

        public IField RightInfoField => _rightInfoField;

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.LeftArrow:
                    _gameField.MoveCurrentFigure(MotionDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                    _gameField.MoveCurrentFigure(MotionDirection.Right);
                    break;
                case ConsoleKey.DownArrow:
                    _gameField.MoveCurrentFigure(MotionDirection.Down);
                    break;
                case ConsoleKey.Spacebar:
                    _gameField.RotateCurrentFigure();
                    break;
                case ConsoleKey.Escape:
                    _gameEngineDisposable.Dispose();
                    _commandObserverDisposable.Dispose();
                    _gameView.NavigationService.Navigate(new HomeView(_gameView.Size));
                    break;
            }
        }
    }
}
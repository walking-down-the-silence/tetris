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
        private readonly IContainer _container;
        private readonly GameView _gameView;
        private IGameField _gameField;
        private IRightInfoField _rightInfoField;
        private GameEngine _gameEngine;
        private ICommandBus _commandBus;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _gameEngineDisposable;
        private IDisposable _commandObserverDisposable;

        public GamePresenter(GameView gameView, IContainer container)
        {
            _gameView = gameView;
            _container = container;
        }

        public void Initialize()
        {
            InitializeGameField();
            InitializeRightInfoField();

            _commandBus = _container.Resolve<ICommandBus>();
            _gameEngine = new GameEngine(_container, _commandBus);
            _gameEngineDisposable = _gameEngine.Run(_gameField);

            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        public IField GameField => _gameField;

        public IField RightInfoField => _rightInfoField;

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
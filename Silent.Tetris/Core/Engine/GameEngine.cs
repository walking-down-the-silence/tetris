using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core.Engine
{
    public class GameEngine : IGameEngine
    {
        private const int LayoutMargin = 1;
        private const int RightPanelWidth = 20;
        private readonly IContainer _container;
        private readonly ICommandBus _commandBus;
        private readonly MotionDetector _motionDetector = new MotionDetector();
        private IGameField _gameField;
        private IGameState _gameState;
        private IFigure _nextFigure;
        private IRandomGenerator<IFigure> _figureRandomGenerator;
        private Disposable _gameEdngineDisposable;
        private int _currentScore;

        public GameEngine(IContainer container, ICommandBus commandBus)
        {
            _container = container;
            _commandBus = commandBus;
        }

        public IGameField Field => _gameField;

        public IGameState State => _gameState;

        public event EventHandler<GameStateEventArgs> StateChanged;

        public IDisposable Run()
        {
            _gameEdngineDisposable = new Disposable();
            _figureRandomGenerator = _container.Resolve<IRandomGenerator<IFigure>>();
            _nextFigure = _figureRandomGenerator.GenerateNext();
            _gameField = InitializeGameField();
            _gameField.SetCurrentFigure(GenerateCurrentFigure());

            _gameState = InitializeRightInfoField();

            ChangeState(_gameField.CurrentFigure, _nextFigure, _currentScore);
            GenerateMoveDownCommandsAsync(500);
            ConsumeCommandsAsync();
            return _gameEdngineDisposable;
        }

        protected void OnStateChanged(GameStateEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }

        private void ChangeState(IFigure currentFigure, IFigure nextFigure, int currentScore)
        {
            State.AssignNextFigure(nextFigure);
            State.SetScore(currentScore);
            OnStateChanged(new GameStateEventArgs(currentFigure, nextFigure, currentScore));
        }

        private void HandleCommand(ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.LeftArrow:
                    MoveCurrentFigure(MotionDirection.Left);
                    break;
                case ConsoleKey.RightArrow:
                    MoveCurrentFigure(MotionDirection.Right);
                    break;
                case ConsoleKey.DownArrow:
                    MoveCurrentFigure(MotionDirection.Down);
                    break;
                case ConsoleKey.Spacebar:
                    RotateCurrentFigure();
                    break;
            }
        }

        private void MoveCurrentFigure(MotionDirection motionDirection)
        {
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_gameField.Ground, _gameField.CurrentFigure);
            
            if (motionDirection == MotionDirection.Down && !allowedMovements.Contains(motionDirection))
            {
                IFigure currentFigure = GenerateCurrentFigure();
                IGround ground = _gameField.Ground.Merge(_gameField.CurrentFigure);
                _gameField.SetCurrentFigure(currentFigure);
                _gameField.SetGround(ground);
                _currentScore = _currentScore + 1000;
                ChangeState(currentFigure, _nextFigure, _currentScore);
            }
            else if (allowedMovements.Contains(motionDirection))
            {
                IFigure afterMovement = _gameField.CurrentFigure.Move(motionDirection);
                _gameField.SetCurrentFigure(afterMovement);
            }
        }

        private void RotateCurrentFigure()
        {
            IFigure afterRotation = _gameField.CurrentFigure.Rotate(RotateDirection.Right90Degrees);
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_gameField.Ground, afterRotation);

            if (allowedMovements.Contains(MotionDirection.Left) &&
                allowedMovements.Contains(MotionDirection.Right) &&
                allowedMovements.Contains(MotionDirection.Down))
            {
                _gameField.SetCurrentFigure(afterRotation);
            }
        }

        private IFigure GenerateCurrentFigure()
        {
            int currentX = _gameField.Ground.Size.Width / 2 - _nextFigure.Size.Width / 2;
            int currentY = _gameField.Ground.Size.Height - _nextFigure.Size.Height;
            IFigure currentFigure = _nextFigure.SetPosition(new Position(currentX, currentY));
            _nextFigure = _figureRandomGenerator.GenerateNext();
            return currentFigure;
        }

        private IGameState InitializeRightInfoField()
        {
            IConfiguration configuration = _container.Resolve<IConfiguration>();
            int gameStateLeft = _gameField.Position.Left + configuration.GameFieldSize.Width + 1;
            Position gameStatePosition = new Position(gameStateLeft, 0);
            Size gameStateSize = new Size(RightPanelWidth, configuration.GameFieldSize.Height);
            return new GameState(gameStatePosition, gameStateSize);
        }

        private IGameField InitializeGameField()
        {
            IConfiguration configuration = _container.Resolve<IConfiguration>();
            Position gameFieldPosition = new Position(LayoutMargin, 0);
            return new GameField(gameFieldPosition, configuration.GameFieldSize);
        }

        private void GenerateMoveDownCommandsAsync(int delay)
        {
            Task.Run(() =>
            {
                while (!_gameEdngineDisposable.IsDisposed)
                {
                    _commandBus.Add(new ConsoleCommand(ConsoleKey.DownArrow));
                    Task.Delay(delay).Wait();
                }
            });
        }

        private void ConsumeCommandsAsync()
        {
            Task.Run(() =>
            {
                while (!_gameEdngineDisposable.IsDisposed)
                {
                    ICommand command = _commandBus.Take();
                    HandleCommand(command);
                }
            });
        }

        private sealed class Disposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}

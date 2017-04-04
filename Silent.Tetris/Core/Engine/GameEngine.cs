using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core.Engine
{
    public class GameEngine : IGameEngine
    {
        private const int LayoutMargin = 1;
        private readonly IContainer _container;
        private readonly MotionDetector _motionDetector = new MotionDetector();
        private IGameField _gameField;
        private IGameState _gameState;
        private IRandomGenerator<IFigure> _figureRandomGenerator;
        private Disposable _gameEngineDisposable;

        public GameEngine(IContainer container)
        {
            _container = container;
        }

        public IGameField Field => _gameField;

        public IGameState State => _gameState;

        public event EventHandler<GameStateEventArgs> StateChanged;

        public IDisposable Run()
        {
            _gameEngineDisposable = new Disposable();
            _figureRandomGenerator = _container.Resolve<IRandomGenerator<IFigure>>();

            _gameState = new GameState();
            _gameState.AssignNextFigure(_figureRandomGenerator.GenerateNext());
            _gameState.SetScore(0);

            _gameField = InitializeGameField();
            _gameField.SetCurrentFigure(GenerateCurrentFigure());

            GenerateMoveDownCommandsAsync(500);
            return _gameEngineDisposable;
        }

        public bool IsGameOver()
        {
            if (_gameField.Ground.Size.Height >= _gameField.Size.Height)
            {
                int y = _gameField.Size.Height - 1;

                for (int i = 0; i < _gameField.Size.Width; i++)
                {
                    if(_gameField.Ground[i, y] != Color.Transparent)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void MoveCurrentFigure(MotionDirection motionDirection)
        {
            if(IsGameOver())
            {
                OnStateChanged();
                return;
            }

            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_gameField.Ground, _gameField.CurrentFigure);

            if (motionDirection == MotionDirection.Down && !allowedMovements.Contains(motionDirection))
            {
                IGround ground = _gameField.Ground.Merge(_gameField.CurrentFigure);
                IFigure currentFigure = GenerateCurrentFigure();

                if (IsGameOver())
                {
                    OnStateChanged();
                    return;
                }

                _gameField.SetCurrentFigure(currentFigure);
                _gameField.SetGround(ground);
                int rowsCompleted = _gameField.Ground.Clean();

                _gameState.AssignNextFigure(_figureRandomGenerator.GenerateNext());
                _gameState.SetScore(_gameState.CurrentScore + 100 * rowsCompleted);
            }
            else if (allowedMovements.Contains(motionDirection))
            {
                IFigure afterMovement = _gameField.CurrentFigure.Move(motionDirection);
                _gameField.SetCurrentFigure(afterMovement);
            }
        }

        public void RotateCurrentFigure()
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

        protected void OnStateChanged()
        {
            GameStateEventArgs eventArgs = new GameStateEventArgs(
                _gameField.CurrentFigure, 
                _gameState.NextFigure, 
                _gameState.CurrentScore);
            StateChanged?.Invoke(this, eventArgs);
        }

        private IFigure GenerateCurrentFigure()
        {
            int currentX = _gameField.Ground.Size.Width / 2 - _gameState.NextFigure.Size.Width / 2;
            int currentY = _gameField.Ground.Size.Height - _gameState.NextFigure.Size.Height;
            return _gameState.NextFigure.SetPosition(new Position(currentX, currentY));
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
                while (!_gameEngineDisposable.IsDisposed && !IsGameOver())
                {
                    MoveCurrentFigure(MotionDirection.Down);
                    Task.Delay(delay).Wait();
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

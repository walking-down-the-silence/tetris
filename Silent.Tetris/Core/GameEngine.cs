using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core
{
    public class GameEngine : IGameEngine
    {
        private readonly IContainer _container;
        private readonly ICommandBus _commandBus;
        private readonly MotionDetector _motionDetector = new MotionDetector();
        private IGameField _gameField;
        private IFigure _nextFigure;
        private IRandomGenerator<IFigure> _figureRandomGenerator;
        private Disposable _gameEdngineDisposable;

        public GameEngine(IContainer container, ICommandBus commandBus)
        {
            _container = container;
            _commandBus = commandBus;
        }

        public IGameField GameField => _gameField;

        public IDisposable Run(IGameField gameField)
        {
            _gameEdngineDisposable = new Disposable();
            _figureRandomGenerator = _container.Resolve<IRandomGenerator<IFigure>>();
            _nextFigure = _figureRandomGenerator.GenerateNext();
            _gameField = gameField;
            _gameField.SetCurrentFigure(GenerateCurrentFigure());
            GenerateMoveDownCommandsAsync(500);
            ConsumeCommandsAsync();
            return _gameEdngineDisposable;
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

        private bool IsInBounds(ISprite sprite)
        {
            return sprite.Position.Bottom >= _gameField.Position.Bottom
                && sprite.Position.Left + sprite.Size.Width < _gameField.Position.Left + _gameField.Size.Width + 1
                && sprite.Position.Left >= _gameField.Position.Left;
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

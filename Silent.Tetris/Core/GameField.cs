using System.Collections.Generic;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core
{
    public class GameField : IGameField
    {
        private readonly FigureRandomGenerator _figureRandomGenerator;
        private readonly MotionDetector _motionDetector = new MotionDetector();
        private readonly Position _nextFigureFieldPosition;
        private readonly Size _nextFigureFieldSize;
        private IFigure _currentFigure;
        private IFigure _nextFigure;
        private IGround _groundFigure;

        public GameField(Size size)
        {
            Size = size;
            _groundFigure = new GoundFigure(Position.None, new Size(Size.Width - 23, Size.Height - 2));
            _nextFigureFieldSize = new Size(20, Size.Height / 2 - 1);
            int nextFIgureFieldX = _groundFigure.Position.Left + _groundFigure.Size.Width + 1;
            int nextFIgureFieldY = _groundFigure.Position.Bottom + _groundFigure.Size.Height - _nextFigureFieldSize.Height;
            _nextFigureFieldPosition = new Position(nextFIgureFieldX, nextFIgureFieldY);
            _figureRandomGenerator = new FigureRandomGenerator(new FigureFactory(), new Position(size.Width / 2, size.Height - 1));
            _nextFigure = _figureRandomGenerator.GenerateNext();
            GenerateNextFigure();
        }

        public Size Size { get; }

        public IFigure CurrentFigure => _currentFigure;

        public IFigure NextFigure => _nextFigure;

        public IGround Ground => _groundFigure;

        public void MoveCurrentFigure(MotionDirection motionDirection)
        {
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_groundFigure, _currentFigure);
            IFigure afterMovement = _currentFigure.Move(motionDirection);

            if (motionDirection == MotionDirection.Down && !allowedMovements.Contains(motionDirection))
            {
                _groundFigure = _groundFigure.Merge(_currentFigure);
                GenerateNextFigure();
            }
            else if (IsInBounds(afterMovement) && allowedMovements.Contains(motionDirection))
            {
                _currentFigure = afterMovement;
            }
        }

        public void RotateCurrentFigure()
        {
            IFigure afterRotation = _currentFigure.Rotate(RotateDirection.Right90Degrees);
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_groundFigure, afterRotation);

            if (IsInBounds(afterRotation) && allowedMovements.Count == 3)
            {
                _currentFigure = afterRotation;
            }
        }

        public void GenerateNextFigure()
        {
            int currentX = _groundFigure.Size.Width / 2 - _nextFigure.Size.Width / 2;
            int currentY = _groundFigure.Size.Height - _nextFigure.Size.Height;
            _currentFigure = _nextFigure.SetPosition(new Position(currentX, currentY));

            IFigure nextFigure = _figureRandomGenerator.GenerateNext();
            int nextX = _nextFigureFieldPosition.Left + _nextFigureFieldSize.Width / 2 - nextFigure.Size.Width / 2;
            int nextY = _nextFigureFieldPosition.Bottom + _nextFigureFieldSize.Height / 2 - nextFigure.Size.Height / 2;

            _nextFigure = nextFigure.SetPosition(new Position(nextX, nextY));
        }

        private bool IsInBounds(ISprite sprite)
        {
            return sprite.Position.Bottom >= 0
                && sprite.Position.Left + sprite.Size.Width < Size.Width
                && sprite.Position.Left >= 0;
        }
    }
}

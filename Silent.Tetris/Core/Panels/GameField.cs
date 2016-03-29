using System.Collections.Generic;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;
using Silent.Tetris.Core.Sprites;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core.Panels
{
    public class GameField : FieldBase, IGameField
    {
        private IFigure _currentFigure;
        private IGround _groundFigure;
        private readonly IRandomGenerator<IFigure> _figureRandomGenerator;
        private readonly MotionDetector _motionDetector = new MotionDetector();

        public GameField(Position position, Size size) : base(position, size)
        {
            _groundFigure = new GoundFigure(position, size);
            _figureRandomGenerator = new FigureRandomGenerator(new FigureFactory());
            GenerateNextFigure();
        }

        public IFigure CurrentFigure => _currentFigure;

        public IGround Ground => _groundFigure;

        public void AssignCurrentFigure(IFigure currentFigure)
        {
            _currentFigure = currentFigure;
        }

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
            IFigure nextFigure = _figureRandomGenerator.GenerateNext();
            int currentX = _groundFigure.Size.Width / 2 - nextFigure.Size.Width / 2;
            int currentY = _groundFigure.Size.Height - nextFigure.Size.Height;
            _currentFigure = nextFigure.SetPosition(new Position(currentX, currentY));
        }

        protected override IEnumerable<ISprite> GetSpriteCollection()
        {
            return new ISprite[] { _groundFigure, _currentFigure };
        }

        private bool IsInBounds(ISprite sprite)
        {
            return sprite.Position.Bottom >= Position.Bottom
                && sprite.Position.Left + sprite.Size.Width < Position.Left + Size.Width + 1
                && sprite.Position.Left >= Position.Left;
        }
    }
}

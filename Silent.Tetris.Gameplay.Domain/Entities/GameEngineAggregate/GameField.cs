using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Practices.DDD;
using Silent.Practices.EventStore;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Extensions;
using Silent.Tetris.Gameplay.Domain.Events;

namespace Silent.Tetris.Core.Engine
{
    public class GameField : EventAggregate<Guid, Event<Guid>>, IGameField, IAggregateRoot
    {
        private readonly MotionDetector _motionDetector = new MotionDetector();

        // TODO: resolve this dependency in a better way
        private readonly IRandomGenerator<IFigure> _randomGenerator = new FigureRandomGenerator(new FigureFactory());
        private readonly Size _size;
        private IFigure _currentFigure;
        private IFigure _nextFigure;
        private IGround _ground;
        private Color[,] _cachedGameFieldView;

        public GameField()
        {
            RegisterEventHandler<GameCreated>(x => { });
            RegisterEventHandler<NewTetrominoAssigned>(x => { });
            RegisterEventHandler<TetrominoMoved>(x => { });
            RegisterEventHandler<TetrominoRotated>(x => { });
            RegisterEventHandler<TetrominoReachedGround>(x => { });
        }

        public GameField(Size size) : this()
        {            
            _size = size;
            _ground = new GoundFigure(Position.None, size);

            // TODO: handle initial figure generation outside from ctor
            _currentFigure = _randomGenerator.GenerateNext();
            _nextFigure = _randomGenerator.GenerateNext();

            Id = Guid.NewGuid();
            ApplyEvent(new GameCreated(Id, Size));
        }

        public Size Size => _size;

        public IFigure CurrentFigure => _currentFigure;

        public IFigure NextFigure => _nextFigure;

        public IGround Ground => _ground;

        public void MoveCurrentFigure(MotionDirection motionDirection)
        {
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_ground, _currentFigure);

            if (motionDirection == MotionDirection.Down && !allowedMovements.Contains(motionDirection))
            {
                _ground = _ground.Merge(_currentFigure);
                int rowsCompleted = _ground.Clean();

                AssignCurrentFigure(_nextFigure);
                _nextFigure = _randomGenerator.GenerateNext();

                ApplyEvent(new TetrominoReachedGround(Id, rowsCompleted));
            }
            else if (allowedMovements.Contains(motionDirection))
            {
                _currentFigure = _currentFigure.Move(motionDirection);
                ApplyEvent(new TetrominoMoved(Id, motionDirection));
            }
        }

        public void RotateCurrentFigure()
        {
            IFigure afterRotation = _currentFigure.Rotate(RotateDirection.Right90Degrees);
            HashSet<MotionDirection> allowedMovements = _motionDetector.DetectAllowedMotion(_ground, afterRotation);

            if (allowedMovements.Contains(MotionDirection.Left) &&
                allowedMovements.Contains(MotionDirection.Right) &&
                allowedMovements.Contains(MotionDirection.Down))
            {
                _currentFigure = afterRotation;
                ApplyEvent(new TetrominoRotated(Id, RotateDirection.Right90Degrees));
            }
        }

        private void AssignCurrentFigure(IFigure newFigure)
        {
            int currentX = _ground.Size.Width / 2 - newFigure.Size.Width / 2;
            int currentY = _ground.Size.Height - newFigure.Size.Height;
            _currentFigure = newFigure.SetPosition(new Position(currentX, currentY));
            ApplyEvent(new NewTetrominoAssigned(Id, _currentFigure.GetType().Name));
        }

        #region Sprite Colors

        public ISprite GetView()
        {
            IEnumerable<ISprite> sprites = new ISprite[] { Ground, CurrentFigure }.Where(x => x != null);
            Color[,] currentGameFieldView = new Color[Size.Height, Size.Width];

            foreach (ISprite sprite in sprites)
            {
                FillColorView(currentGameFieldView, sprite);
            }

            Color[,] differenceGameFieldView = _cachedGameFieldView == null
                ? currentGameFieldView
                : ColorArrayTransformer.GetDifference(_cachedGameFieldView, currentGameFieldView);

            _cachedGameFieldView = currentGameFieldView;

            return CreateFieldSprite(differenceGameFieldView);
        }
        
        private void FillColorView(Color[,] colorView, ISprite sprite)
        {
            for (int i = 0; i < sprite.Size.Width; i++)
            {
                for (int j = 0; j < sprite.Size.Height; j++)
                {
                    int xPosition = sprite.Position.Left + i;
                    int yPosition = Size.Height - sprite.Position.Bottom - j - 1;

                    if (xPosition >= 0 &&
                        xPosition < colorView.GetLength(1) &&
                        yPosition >= 0 &&
                        yPosition < colorView.GetLength(0) &&
                        sprite[i, j] != Color.Transparent)
                    {
                        colorView[yPosition, xPosition] = sprite[i, j];
                    }
                }
            }
        }

        private ISprite CreateFieldSprite(Color[,] colors)
        {
            return new FieldSprite(Position.None, colors);
        }

        private class FieldSprite : ISprite
        {
            private readonly Color[,] _colors;

            public FieldSprite(Position position, Color[,] colors)
            {
                _colors = colors;
                Position = position;
                Size = new Size(colors.GetLength(1), colors.GetLength(0));
            }

            public Color this[int x, int y] => _colors[Size.Height - y - 1, x];

            public Position Position { get; }

            public Size Size { get; }
        }

        #endregion
    }
}

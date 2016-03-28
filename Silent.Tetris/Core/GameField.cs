using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;
using Silent.Tetris.Extensions;

namespace Silent.Tetris.Core
{
    public class GameField : IGameField
    {
        #region Private Fields

        private readonly FigureRandomGenerator _figureRandomGenerator;
        private readonly MotionDetector _motionDetector = new MotionDetector();
        private readonly Position _nextFigureFieldPosition;
        private readonly Size _nextFigureFieldSize;
        private readonly IList<IFigure> _scoreWordCharacters;
        private readonly IList<IFigure> _nextWordCharacters;
        private readonly IList<IFigure> _scoreNumberCharacters;
        private IFigure _currentFigure;
        private IFigure _nextFigure;
        private IGround _groundFigure;
        private Color[,] _cachedGameFieldView;

        #endregion

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

            IFactoryMethod<IEnumerable<IFigure>, string> symbolFactory = new SymbolFactory();
            _scoreWordCharacters = symbolFactory.Create("Score").ToList();
            _nextWordCharacters = symbolFactory.Create("Next").ToList();
            _scoreNumberCharacters = symbolFactory.Create("17000").ToList();

            int initialPositionX = _groundFigure.Position.Left + _groundFigure.Size.Width + 2;
            int initialPositionY = _groundFigure.Position.Bottom + _groundFigure.Size.Height / 2 + 2;

            PositionCharacters(initialPositionX, initialPositionY, _nextWordCharacters);
            PositionCharacters(initialPositionX, initialPositionY - 6, _scoreWordCharacters);
            PositionCharacters(initialPositionX, initialPositionY - 12, _scoreNumberCharacters);
        }

        #region Public Properties

        public Size Size { get; }

        public IFigure CurrentFigure => _currentFigure;

        public IFigure NextFigure => _nextFigure;

        public IGround Ground => _groundFigure;

        #endregion

        public ISprite GetView()
        {
            Color[,] currentGameFieldView = new Color[Size.Height, Size.Width];
            IEnumerable<ISprite> sprites = new ISprite[]
                {
                    _currentFigure,
                    //_nextFigure,
                    //_groundFigure,
                };
                //.Concat(_scoreWordCharacters)
                //.Concat(_nextWordCharacters)
                //.Concat(_scoreNumberCharacters);

            foreach (ISprite sprite in sprites)
            {
                FillColorView(currentGameFieldView, sprite);
            }

            Color[,] differenceGameFieldView = _cachedGameFieldView == null
                ? currentGameFieldView
                : GetViewDifference(_cachedGameFieldView, currentGameFieldView);

            _cachedGameFieldView = currentGameFieldView;

            return new GameFieldSprite(differenceGameFieldView);
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
            int currentX = _groundFigure.Size.Width / 2 - _nextFigure.Size.Width / 2;
            int currentY = _groundFigure.Size.Height - _nextFigure.Size.Height;
            _currentFigure = _nextFigure.SetPosition(new Position(currentX, currentY));

            IFigure nextFigure = _figureRandomGenerator.GenerateNext();
            int nextX = _nextFigureFieldPosition.Left + _nextFigureFieldSize.Width / 2 - nextFigure.Size.Width / 2;
            int nextY = _nextFigureFieldPosition.Bottom + _nextFigureFieldSize.Height / 2 - nextFigure.Size.Height / 2;

            _nextFigure = nextFigure.SetPosition(new Position(nextX, nextY));
        }

        #region Private Methods

        private bool IsInBounds(ISprite sprite)
        {
            return sprite.Position.Bottom >= 0
                && sprite.Position.Left + sprite.Size.Width < Size.Width
                && sprite.Position.Left >= 0;
        }

        private void PositionCharacters(int nextFigureFieldX, int nextFigureFieldY, IList<IFigure> characters)
        {
            int letterPositionX = nextFigureFieldX;

            for (int index = 0; index < characters.Count; index++)
            {
                int letterPositionY = nextFigureFieldY - characters[index].Size.Height;

                Position letterPosition = new Position(letterPositionX, letterPositionY);
                characters[index] = characters[index].SetPosition(letterPosition);

                letterPositionX = letterPositionX + characters[index].Size.Width + 1;
            }
        }

        private void FillColorView(Color[,] colorView, ISprite sprite)
        {
            for (int i = 0; i < sprite.Size.Width; i++)
            {
                for (int j = 0; j < sprite.Size.Height; j++)
                {
                    int xPosition = sprite.Position.Left + i;
                    int yPosition = Size.Height - sprite.Position.Bottom - j - 1;
                    colorView[yPosition, xPosition] = sprite[i, j];
                }
            }
        }

        private Color[,] GetViewDifference(Color[,] previousView, Color[,] currentView)
        {
            Color[,] differenceView = new Color[Size.Height, Size.Width];

            for (int i = 0; i < Size.Width; i++)
            {
                for (int j = 0; j < Size.Height; j++)
                {
                    if (previousView[j, i] != currentView[j, i])
                    {
                        if (currentView[j, i] == Color.Transparent)
                        {
                            differenceView[j, i] = Color.Black;
                        }
                        else
                        {
                            differenceView[j, i] = currentView[j, i];
                        }
                    }
                }
            }

            return differenceView;
        }

        #endregion

        private class GameFieldSprite : ISprite
        {
            private readonly Color[,] _colors;
            private readonly Position _position;
            private readonly Size _size;

            public GameFieldSprite(Color[,] colors)
            {
                _colors = colors;
                _position = Position.None;
                _size = new Size(colors.GetLength(1), colors.GetLength(0));
            }

            public Color this[int x, int y] => _colors[_size.Height - y - 1, x];

            public Position Position => _position;

            public Size Size => _size;
        }
    }
}

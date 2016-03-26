using System;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public abstract class FigureBase : IFigure
    {
        private readonly Color[,] _cells;
        private readonly Position _position;
        private readonly Size _size;

        protected FigureBase(Position position, Color[,] cells)
        {
            _cells = cells;
            _position = position;
            _size = new Size(cells.GetLength(1), cells.GetLength(0));
        }

        public Color this[int x, int y] => _cells[y, x];

        public Position Position => _position;

        public Size Size => _size;

        public virtual IFigure Clone(params object[] parameters)
        {
            return Activator.CreateInstance(GetType(), parameters) as IFigure;
        }

        public IFigure SetPosition(Position position)
        {
            IFigure cloned = Clone(position, _cells);
            return cloned;
        }

        public virtual IFigure Rotate(RotateDirection rotateDirection)
        {
            Color[,] newArray;

            switch (rotateDirection)
            {
                case RotateDirection.Right90Degrees:
                    newArray = Rotate90Right();
                    break;
                case RotateDirection.Left90Degrees:
                    newArray = Rotate90Left();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotateDirection), rotateDirection, null);
            }

            IFigure cloned = Clone(_position, newArray);
            return cloned;
        }

        private Color[,] Rotate90Right()
        {
            int height = _size.Width;
            int width = _size.Height;
            Color[,] newArray = new Color[height, width];

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    newArray[i, j] = _cells[width - j - 1, i];
                }
            }

            return newArray;
        }

        private Color[,] Rotate90Left()
        {
            int height = _size.Width;
            int width = _size.Height;
            Color[,] newArray = new Color[height, width];

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    newArray[i, j] = _cells[j, height - i - 1];
                }
            }

            return newArray;
        }
    }
}
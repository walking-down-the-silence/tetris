using System.Collections.Generic;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Core.Panels
{
    public abstract class FieldBase : IField
    {
        private readonly Position _position;
        private readonly Size _size;
        private Color[,] _cachedGameFieldView;

        protected FieldBase(Position position, Size size)
        {
            _position = position;
            _size = size;
        }

        public Position Position => _position;

        public Size Size => _size;

        public ISprite GetView()
        {
            IEnumerable<ISprite> sprites = GetSpriteCollection();
            Color[,] currentGameFieldView = new Color[Size.Height, Size.Width];

            foreach (ISprite sprite in sprites)
            {
                FillColorView(currentGameFieldView, sprite);
            }

            Color[,] differenceGameFieldView = _cachedGameFieldView == null
                ? currentGameFieldView
                : GetViewDifference(_cachedGameFieldView, currentGameFieldView);

            _cachedGameFieldView = currentGameFieldView;

            return CreateFieldSprite(Position, differenceGameFieldView);
        }

        protected abstract IEnumerable<ISprite> GetSpriteCollection();

        protected void FillColorView(Color[,] colorView, ISprite sprite)
        {
            for (int i = 0; i < sprite.Size.Width; i++)
            {
                for (int j = 0; j < sprite.Size.Height; j++)
                {
                    int xPosition = sprite.Position.Left - Position.Left + i;
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

        protected Color[,] GetViewDifference(Color[,] previousView, Color[,] currentView)
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

        protected ISprite CreateFieldSprite(Position position, Color[,] colors)
        {
            return new FieldSprite(position, colors);
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
    }
}
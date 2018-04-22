using System;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Rendering;

namespace Silent.Tetris.Renderers
{
    public class SpriteRenderer : ISpriteRenderer
    {
        private readonly ColorConverter _colorConverter = new ColorConverter();

        public SpriteRenderer()
        {
            Console.CursorVisible = false;
            Console.Clear();
        }

        public void Render(ISprite source)
        {
            if(source == null)
                return;

            const string emptyCell = "  ";
            Console.CursorVisible = false;
            Console.ResetColor();

            for (int x = 0; x < source.Size.Width; x++)
            {
                for (int y = 0; y < source.Size.Height; y++)
                {
                    if (source[x, y] != Color.Transparent)
                    {
                        Console.BackgroundColor = _colorConverter.Convert(source[x, y]);
                        int xPosition = (source.Position.Left + x) * emptyCell.Length;
                        int yPosition = source.Position.Bottom + y;
                        ConsoleHelper.WriteAtPosition(xPosition, yPosition, emptyCell);
                    }
                }
            }

            Console.ResetColor();
        }
    }
}
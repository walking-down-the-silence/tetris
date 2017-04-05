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
            Console.Clear();
        }

        public void Render(ISprite sprite)
        {
            if(sprite == null)
                return;

            const string emptyCell = "  ";
            Console.CursorVisible = false;
            Console.ResetColor();

            for (int x = 0; x < sprite.Size.Width; x++)
            {
                for (int y = 0; y < sprite.Size.Height; y++)
                {
                    if (sprite[x, y] != Color.Transparent)
                    {
                        Console.BackgroundColor = _colorConverter.Convert(sprite[x, y]);
                        int xPosition = (sprite.Position.Left + x) * emptyCell.Length;
                        int yPosition = sprite.Position.Bottom + y;
                        ConsoleHelper.WriteAtPosition(xPosition, yPosition, emptyCell);
                    }
                }
            }

            Console.ResetColor();
        }
    }
}
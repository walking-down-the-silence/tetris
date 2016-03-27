using System;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core
{
    public class ColorConverter
    {
        public ConsoleColor Convert(Color color)
        {
            switch (color)
            {
                case Color.Black:
                    return ConsoleColor.Black;
                case Color.White:
                    return ConsoleColor.White;
                case Color.Gray:
                    return ConsoleColor.Gray;
                case Color.Green:
                    return ConsoleColor.Green;
                case Color.Blue:
                    return ConsoleColor.Blue;
                case Color.Red:
                    return ConsoleColor.Red;
                case Color.Yellow:
                    return ConsoleColor.Yellow;
                case Color.Magenta:
                    return ConsoleColor.Magenta;
                case Color.Cyan:
                    return ConsoleColor.Cyan;
                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, null);
            }
        }
    }
}
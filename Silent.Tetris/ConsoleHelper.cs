using System;

namespace Silent.Tetris
{
    public static class ConsoleHelper
    {
        public static void WriteAtPosition(int x, int y, string cell)
        {
            if (IsInBounds(x, Console.WindowHeight - y - 1))
            {
                Console.SetCursorPosition(x, Console.WindowHeight - y - 1);
                Console.Write(cell);
            }
        }

        public static bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight;
        }
    }
}
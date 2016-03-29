using System;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class ConsoleCommand : ICommand
    {
        private readonly ConsoleKey _key;
        private readonly string _name;

        public ConsoleCommand(string name)
        {
            _name = name;
        }

        public ConsoleCommand(ConsoleKey key)
        {
            _key = key;
        }

        public string Name => $"{_name ?? _key.ToString()} Command";

        public ConsoleKey Key => _key;
    }
}

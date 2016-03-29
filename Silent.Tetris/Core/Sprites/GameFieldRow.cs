using System.Linq;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Core.Sprites
{
    public class GameFieldRow : IGameFieldRow
    {
        private readonly Color[] _cells;

        public GameFieldRow(int length)
        {
            _cells = new Color[length];
        }

        public Color this[int index]
        {
            get { return _cells[index]; }
            set { _cells[index] = value; }
        }

        public bool IsComplete()
        {
            return _cells.All(cell => cell != Color.Transparent);
        }
    }
}
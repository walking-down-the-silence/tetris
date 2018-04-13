using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class GoundFigure : IGround
    {
        private readonly IList<IGameFieldRow> _gameFieldRows;
        private readonly Position _position;
        private readonly Size _size;

        public GoundFigure(Position position, Size size)
        {
            _gameFieldRows = new List<IGameFieldRow>(size.Height);
            _position = position;
            _size = size;
        }

        public Color this[int x, int y]
        {
            get
            {
                if (y >= 0 &&
                    y < _gameFieldRows.Count &&
                    x >= 0 &&
                    x < _size.Width)
                {
                    return _gameFieldRows[y][x];
                }

                return Color.Transparent;
            }
        }

        public Position Position => _position;

        public Size Size => _size;
        
        public IGround Merge(IFigure figure)
        {
            while (_gameFieldRows.Count < figure.Position.Bottom + figure.Size.Height)
            {
                _gameFieldRows.Add(new GameFieldRow(_size.Width));
            }

            for (int i = 0; i < figure.Size.Width; ++i)
            {
                for (int j = 0; j < figure.Size.Height; ++j)
                {
                    int y = figure.Position.Bottom - _position.Bottom + figure.Size.Height - j - 1;
                    int x = figure.Position.Left- _position.Left + i;

                    if (_gameFieldRows[y][x] == Color.Transparent)
                    {
                        _gameFieldRows[y][x] = figure[i, figure.Size.Height - j - 1];
                    }
                }
            }

            return this;
        }

        public int Clean()
        {
            int completedRows = _gameFieldRows.Count(row => row.IsComplete());
            IGameFieldRow completeRow = _gameFieldRows.FirstOrDefault(row => row.IsComplete());

            while (completeRow != null)
            {
                _gameFieldRows.Remove(completeRow);
                completeRow = _gameFieldRows.FirstOrDefault(row => row.IsComplete());
            }

            return completedRows;
        }
    }
}
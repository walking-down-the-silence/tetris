using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris
{
    public class FigureRandomGenerator : IRandomGenerator<IFigure>, IDisposable
    {
        private readonly Random _randomGenerator = new Random();
        private readonly IList<IFigure> _figures;
        private readonly IEnumerator<IFigure> _spriteEnumerator;
        private bool _isDisposed;

        public FigureRandomGenerator(IFactory<IFigure> figureFactory)
        {
            _figures = figureFactory.Create().ToList();
            _spriteEnumerator = GetEnumerator();
        }

        public IFigure GenerateNext()
        {
            _spriteEnumerator.MoveNext();
            return _spriteEnumerator.Current;
        }

        public void Dispose()
        {
            _isDisposed = true;
        }

        private IEnumerator<IFigure> GetEnumerator()
        {
            while (!_isDisposed)
            {
                int nextFigureIndex = _randomGenerator.Next(_figures.Count);
                IFigure nextRandomFigure = _figures[nextFigureIndex];
                yield return nextRandomFigure;
            }
        }
    }
}
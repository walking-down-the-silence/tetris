using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class FigureRandomGenerator : IRandomGenerator<IFigure>, IDisposable
    {
        private readonly Random _randomGenerator = new Random();
        private readonly IFactory<IFigure> _figureFactory;
        private List<IFigure> _figures;
        private bool _isDisposed;

        public FigureRandomGenerator(IFactory<IFigure> figureFactory)
        {
            _figureFactory = figureFactory;
        }

        public IFigure GenerateNext()
        {
            if (_figures == null)
            {
                _figures = _figureFactory.Create().ToList();
            }

            int nextFigureIndex = _randomGenerator.Next(_figures.Count);
            IFigure nextRandomFigure = _figures[nextFigureIndex];
            return nextRandomFigure;
        }

        public void Dispose()
        {
            _isDisposed = true;
        }
    }
}
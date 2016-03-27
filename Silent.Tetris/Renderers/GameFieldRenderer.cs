using System.Collections.Generic;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Core;

namespace Silent.Tetris.Renderers
{
    public class GameFieldRenderer : IGameFieldRenderable
    {
        private readonly SpriteRenderer _spriteRenderer;
        private readonly IEnumerable<IFigure> _scoreWordLetters;
        private readonly IEnumerable<IFigure> _nextWordLetters;

        public GameFieldRenderer()
        {
            _spriteRenderer = new SpriteRenderer();
            IFactoryMethod<IEnumerable<IFigure>, string> symbolFactory = new SymbolFactory();
            _scoreWordLetters = symbolFactory.Create("Score");
            _nextWordLetters = symbolFactory.Create("Next");
        }

        public void Render(IGameField gameField)
        {
            _spriteRenderer.Render(gameField.Ground);
            _spriteRenderer.Render(gameField.CurrentFigure);
            _spriteRenderer.Render(gameField.NextFigure);

            int nextFigureFieldX = gameField.Ground.Position.Left + gameField.Ground.Size.Width + 2;
            int nextFigureFieldY = gameField.Ground.Position.Bottom + gameField.Ground.Size.Height / 2 + 2;

            RenderWord(nextFigureFieldX, nextFigureFieldY, _nextWordLetters);
            RenderWord(nextFigureFieldX, nextFigureFieldY - 6, _scoreWordLetters);
        }

        private void RenderWord(int nextFigureFieldX, int nextFigureFieldY, IEnumerable<IFigure> scoreWordLetters)
        {
            int letterPositionX = nextFigureFieldX;

            foreach (IFigure letter in scoreWordLetters)
            {
                int letterPositionY = nextFigureFieldY - letter.Size.Height;

                Position letterPosition = new Position(letterPositionX, letterPositionY);
                _spriteRenderer.Render(letter.SetPosition(letterPosition));

                letterPositionX = letterPositionX + letter.Size.Width + 1;
            }
        }
    }
}
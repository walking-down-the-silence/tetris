using System;
using System.Collections.Generic;
using System.Linq;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Presenters;
using Silent.Tetris.Core.Engine;

namespace Silent.Tetris.Views
{
    public class GameOverView : IGameOverView
    {
        private readonly IContainer _container;
        private readonly int _score;
        private ISpriteRenderer _gameFieldRenderable;
        private IFactory<IEnumerable<IFigure>, string> _symbolFactory;
        private IList<IFigure> _gameCharacters;
        private IList<IFigure> _overCharacters;
        private IList<IFigure> _scoreCharacters;

        public GameOverView(IContainer container, int score)
        {
            _container = container;
            _score = score;
            Size = new Size(32, 22);
        }

        public Size Size { get; }

        public IGameOverPresenter Presenter { get; private set; }

        public void Initialize()
        {
            _gameFieldRenderable = _container.Resolve<ISpriteRenderer>();
            _symbolFactory = new SymbolFactory();

            _gameCharacters = _symbolFactory.Create("GAME").ToList();
            _overCharacters = _symbolFactory.Create("OVER").ToList();
            _scoreCharacters = _symbolFactory.Create(_score.ToString()).ToList();

            Presenter = new GameOverPresenter(_container, _score);
            Presenter.Initialize();
        }

        public void Render()
        {
            Console.Clear();

            int left = 1;
            int top = Size.Height - _gameCharacters[0].Size.Height - 1;

            for (int i = 0; i < _gameCharacters.Count; i++)
            {
                var symbol = _gameCharacters[i].SetPosition(new Position(left, top));
                left = left + _gameCharacters[i].Size.Width + 1;
                _gameFieldRenderable.Render(symbol);
            }

            left = 1;
            top = top - _overCharacters[0].Size.Height - 1;

            for (int i = 0; i < _overCharacters.Count; i++)
            {
                var symbol = _overCharacters[i].SetPosition(new Position(left, top));
                left = left + _overCharacters[i].Size.Width + 1;
                _gameFieldRenderable.Render(symbol);
            }

            left = 1;
            top = top - _scoreCharacters[0].Size.Height - 1;

            for (int i = 0; i < _scoreCharacters.Count; i++)
            {
                var symbol = _scoreCharacters[i].SetPosition(new Position(left, top));
                left = left + _scoreCharacters[i].Size.Width + 1;
                _gameFieldRenderable.Render(symbol);
            }
        }
    }
}

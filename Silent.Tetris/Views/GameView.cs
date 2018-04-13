using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;
using System;

namespace Silent.Tetris.Views
{
    public class GameView : IGameView
    {
        private readonly IDependencyResolver _container;
        private ISpriteRenderer _gameFieldRenderable;

        public GameView(IDependencyResolver container)
        {
            _container = container;
            Size = new Size(10, 22);
        }

        public Size Size { get; }

        public IGamePresenter Presenter { get; private set; }

        public void Initialize()
        {
            _gameFieldRenderable = _container.Resolve<ISpriteRenderer>();

            Presenter = new GamePresenter(_container);
            Presenter.Initialize();

            Console.ResetColor();
            Console.Clear();
        }

        public void Render()
        {
            if(Presenter.Field != null)
            {
                _gameFieldRenderable.Render(Presenter.Field.GetView());
            }
        }
    }
}

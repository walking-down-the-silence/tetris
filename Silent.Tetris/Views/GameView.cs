using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class GameView : IGameView
    {
        private readonly IContainer _container;
        private ISpriteRenderable _gameFieldRenderable;

        public GameView(IContainer container)
        {
            _container = container;
            Size = new Size(32, 22);
        }

        public Size Size { get; private set; }

        public IGamePresenter Presenter { get; private set; }

        public void Initialize()
        {
            _gameFieldRenderable = _container.Resolve<ISpriteRenderable>();

            Presenter = new GamePresenter(_container);
            Presenter.Initialize();
        }

        public void Render()
        {
            _gameFieldRenderable.Render(Presenter.Field.GetView());

            int left = Presenter.Field.Position.Left + Presenter.Field.Size.Width * 2 + 1;
            int top = 1;

            Console.SetCursorPosition(left, top);
            Console.Write("Next Figure");

            top = Size.Height - 1;

            Position newPosition = new Position(left + 1, top - Presenter.State.NextFigure.Size.Height);
            IFigure nextFigure = Presenter.State.NextFigure.SetPosition(newPosition);
            _gameFieldRenderable.Render(nextFigure);

            Console.SetCursorPosition(left + 1, Presenter.State.NextFigure.Size.Height + 2);
            Console.Write($"Score: {Presenter.State.CurrentScore}");
        }
    }
}

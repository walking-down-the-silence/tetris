using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Presenters;
using Silent.Tetris.Renderers;

namespace Silent.Tetris.Views
{
    public class GameView : IGameView
    {
        public GameView(Size size)
        {
            Size = size;
        }

        public INavigationService NavigationService { get; private set; }

        public Size Size { get; }

        public IGamePresenter Presenter { get; private set; }

        public void Initialize(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Presenter = new GamePresenter(this);
            Presenter.Initialize();
        }

        public void Render()
        {
            ISpriteRenderable gameFieldRenderable = new SpriteRenderer();
            gameFieldRenderable.Render(Presenter.GameField.GetView());
        }
    }
}

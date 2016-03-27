using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Rendering;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Core.Renderers;
using Silent.Tetris.Presenters;

namespace Silent.Tetris.Views
{
    public class GameView : IGameView
    {
        public INavigationService NavigationService { get; private set; }

        public IGamePresenter Presenter { get; private set; }

        public void Initialize(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Presenter = new GamePresenter(this);
            Presenter.Initialize();
        }

        public void Render()
        {
            Console.Clear();

            IGameFieldRenderable gameFieldRenderable = new GameFieldRenderer();
            gameFieldRenderable.Render(Presenter.GameField);
        }
    }
}

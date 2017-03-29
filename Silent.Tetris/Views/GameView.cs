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
        private readonly ISpriteRenderable _gameFieldRenderable;

        public GameView(IContainer container)
        {
            _container = container;
            _gameFieldRenderable = container.Resolve<ISpriteRenderable>();
        }

        public INavigationService NavigationService { get; private set; }

        public Size Size { get; private set; }

        public IGamePresenter Presenter { get; private set; }

        public void Initialize(INavigationService navigationService)
        {
            NavigationService = navigationService;
            Presenter = new GamePresenter(navigationService, _container);
            Presenter.Initialize();

            int width = Presenter.Field.Size.Width + Presenter.State.Size.Width;
            int height = Presenter.Field.Size.Height;
            Size = new Size(width, height);
        }

        public void Render()
        {
            _gameFieldRenderable.Render(Presenter.Field.GetView());
            _gameFieldRenderable.Render(Presenter.State.GetView());
        }
    }
}

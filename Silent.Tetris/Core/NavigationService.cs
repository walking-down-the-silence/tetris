using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Core
{
    public class NavigationService : INavigationService
    {
        public IView CurrentView { get; private set; }

        public void Navigate(IView view)
        {
            CurrentView = view;
            CurrentView?.Initialize(this);
        }
    }
}

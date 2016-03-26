using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Contracts
{
    public interface INavigationService
    {
        IView CurrentView { get; }

        void Navigate(IView view);
    }
}
using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Provides the ability to navigate between views.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Get the current view.
        /// </summary>
        IView CurrentView { get; }

        /// <summary>
        /// Navigates to new view.
        /// </summary>
        /// <param name="view"> The view instance. </param>
        void Navigate(IView view);
    }
}
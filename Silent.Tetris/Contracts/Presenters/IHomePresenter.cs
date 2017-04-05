namespace Silent.Tetris.Contracts.Presenters
{
    /// <summary>
    /// Presenter for the home view.
    /// </summary>
    public interface IHomePresenter : IPresenter
    {
        /// <summary>
        /// Gets the menu options list.
        /// </summary>
        MenuOptions[] Options { get; }

        /// <summary>
        /// Gets the selected menu option.
        /// </summary>
        MenuOptions SelectedOption { get; }
    }
}
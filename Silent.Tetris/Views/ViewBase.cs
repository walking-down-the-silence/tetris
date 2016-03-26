using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Views
{
    public abstract class ViewBase<TPresenter> : IView<TPresenter>
    {
        protected int MenuOptionIndex;

        protected MenuOptions[] Options;

        protected ViewBase(MenuOptions[] options)
        {
            Options = options;
        }

        public INavigationService NavigationService { get; protected set; }

        public TPresenter Presenter { get; protected set; }

        public MenuOptions SelectedOption => Options[MenuOptionIndex];

        public abstract void Initialize(INavigationService navigationService);

        public abstract void Render();

        public void SelectNextOption()
        {
            MenuOptionIndex++;
            if (MenuOptionIndex >= Options.Length)
            {
                MenuOptionIndex = 0;
            }
        }

        public void SelectPreviousOption()
        {
            MenuOptionIndex--;
            if (MenuOptionIndex < 0)
            {
                MenuOptionIndex = Options.Length - 1;
            }
        }
    }
}
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Views;

namespace Silent.Tetris.Views
{
    public abstract class ViewBase<TPresenter> : IView<TPresenter>
    {
        protected int MenuOptionIndex;

        protected MenuOptions[] Options;

        protected ViewBase(Size size, MenuOptions[] options)
        {
            Size = size;
            Options = options;
        }

        public INavigationService NavigationService { get; protected set; }

        public Size Size { get; }

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
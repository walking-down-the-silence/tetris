using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class HomePresenter : IHomePresenter
    {
        private readonly IContainer _container;
        private INavigationService _navigationService;
        private IObserveAsync<ICommand> _commandObserver;
        private int _menuOptionIndex;

        public HomePresenter(IContainer container)
        {
            _container = container;
            Options = new[]
            {
                MenuOptions.StartGame,
                MenuOptions.HighScores,
                MenuOptions.Exit
            };
        }

        public MenuOptions[] Options { get; }

        public MenuOptions SelectedOption => Options[_menuOptionIndex];

        public void Initialize()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _commandObserver = _container.Resolve<IObserveAsync<ICommand>>();
            _commandObserver.Update += Handle;
        }

        private void SelectNextOption()
        {
            _menuOptionIndex++;
            if (_menuOptionIndex >= Options.Length)
            {
                _menuOptionIndex = 0;
            }
        }

        private void SelectPreviousOption()
        {
            _menuOptionIndex--;
            if (_menuOptionIndex < 0)
            {
                _menuOptionIndex = Options.Length - 1;
            }
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.DownArrow:
                    SelectNextOption();
                    break;
                case ConsoleKey.UpArrow:
                    SelectPreviousOption();
                    break;
                case ConsoleKey.Enter:
                    _commandObserver.Update -= Handle;
                    IView nextView = GetViewFromMenuOption(SelectedOption);
                    _navigationService.Navigate(nextView);
                    break;
                case ConsoleKey.Escape:
                    _commandObserver.Update -= Handle;
                    _navigationService.Navigate(null);
                    break;
            }
        }

        private IView GetViewFromMenuOption(MenuOptions selectedOption)
        {
            IView view = null;

            switch (selectedOption)
            {
                case MenuOptions.StartGame:
                    view = new GameView(_container);
                    break;
                case MenuOptions.HighScores:
                    view = new HighScoresView(_container);
                    break;
            }

            return view;
        }
    }
}
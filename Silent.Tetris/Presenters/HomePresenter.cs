using System;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Contracts.Views;
using Silent.Tetris.Observers;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class HomePresenter : IHomePresenter
    {
        private readonly HomeView _homeView;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _commandObserverDisposable;

        public HomePresenter(HomeView homeView)
        {
            _homeView = homeView;
        }

        public void Initialize()
        {
            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        public void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.DownArrow:
                    _homeView.SelectNextOption();
                    break;
                case ConsoleKey.UpArrow:
                    _homeView.SelectPreviousOption();
                    break;
                case ConsoleKey.Enter:
                    _commandObserverDisposable.Dispose();
                    IView nextView = GetViewFromMenuOption(_homeView.SelectedOption);
                    _homeView.NavigationService.Navigate(nextView);
                    break;
                case ConsoleKey.Escape:
                    _commandObserverDisposable.Dispose();
                    _homeView.NavigationService.Navigate(null);
                    break;
            }
        }

        private IView GetViewFromMenuOption(MenuOptions selectedOption)
        {
            IView view = null;

            switch (selectedOption)
            {
                case MenuOptions.StartGame:
                    view = new GameView(_homeView.Size);
                    break;
                case MenuOptions.HighScores:
                    view = new HighScoresView(_homeView.Size);
                    break;
            }

            return view;
        }
    }
}
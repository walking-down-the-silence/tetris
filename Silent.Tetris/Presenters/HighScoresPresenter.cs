using System;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Observers;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class HighScoresPresenter : IHighScoresPresenter
    {
        private readonly HighScoresView _highScoresView;
        private readonly IContainer _container;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _commandObserverDisposable;

        public HighScoresPresenter(HighScoresView highScoresView, IContainer container)
        {
            _highScoresView = highScoresView;
            _container = container;
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
                case ConsoleKey.Escape:
                case ConsoleKey.Enter:
                    _commandObserverDisposable.Dispose();
                    _highScoresView.NavigationService.Navigate(new HomeView(_highScoresView.Size, _container));
                    break;
            }
        }
    }
}
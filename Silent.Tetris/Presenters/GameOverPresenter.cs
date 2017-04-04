using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;
using System;

namespace Silent.Tetris.Presenters
{
    public class GameOverPresenter : IGameOverPresenter
    {
        private readonly IContainer _container;
        private INavigationService _navigationService;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _commandObserverDisposable;

        public GameOverPresenter(IContainer container, int score)
        {
            _container = container;
            Score = score;
        }

        public int Score { get; }

        public void Initialize()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _consoleCommandObserveAsync = _container.Resolve<IObserveAsync<ICommand>>();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Enter:
                    _commandObserverDisposable.Dispose();
                    _navigationService.Navigate(new HighScoresView(_container));
                    break;
            }
        }
    }
}

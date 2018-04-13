using System;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class GameOverPresenter : IGameOverPresenter
    {
        private readonly IDependencyResolver _container;
        private INavigationService _navigationService;
        private IObserveAsync<ICommand> _commandObserver;

        public GameOverPresenter(IDependencyResolver container, int score)
        {
            _container = container;
            Score = score;
        }

        public int Score { get; }

        public void Initialize()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _commandObserver = _container.Resolve<IObserveAsync<ICommand>>();
            _commandObserver.Update += Handle;
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Enter:
                    _commandObserver.Update -= Handle;
                    _navigationService.Navigate(new HighScoresView(_container));
                    break;
            }
        }
    }
}

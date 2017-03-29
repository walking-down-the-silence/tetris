using System;
using System.Collections.Generic;
using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Presenters;
using Silent.Tetris.Core.Engine;
using Silent.Tetris.Views;

namespace Silent.Tetris.Presenters
{
    public class HighScoresPresenter : IHighScoresPresenter
    {
        private readonly INavigationService _navigationService;
        private readonly IContainer _container;
        private IObserveAsync<ICommand> _consoleCommandObserveAsync;
        private IDisposable _commandObserverDisposable;
        private IRepository<Player> _playerScoresRepository;

        public HighScoresPresenter(INavigationService navigationService, IContainer container)
        {
            _navigationService = navigationService;
            _container = container;
        }

        public ICollection<Player> HighScores => _playerScoresRepository.GetAll();

        public void Initialize()
        {
            _playerScoresRepository = _container.Resolve<IRepository<Player>>();
            _playerScoresRepository.Load();

            _consoleCommandObserveAsync = new ConsoleCommandsObserveAsync();
            _consoleCommandObserveAsync.Update += Handle;
            _commandObserverDisposable = _consoleCommandObserveAsync.ObserveAsync();
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.Enter:
                    _commandObserverDisposable.Dispose();
                    _navigationService.Navigate(new HomeView(_container));
                    break;
            }
        }
    }
}
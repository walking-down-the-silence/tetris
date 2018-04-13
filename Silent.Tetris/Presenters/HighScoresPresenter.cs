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
        private readonly IDependencyResolver _container;
        private INavigationService _navigationService;
        private IObserveAsync<ICommand> _commandObserver;
        private IRepository<ScoreRecord> _playerScoresRepository;

        public HighScoresPresenter(IDependencyResolver container)
        {
            _container = container;
        }

        public ICollection<ScoreRecord> HighScores => _playerScoresRepository.GetAll();

        public void Initialize()
        {
            _playerScoresRepository = _container.Resolve<IRepository<ScoreRecord>>();
            _playerScoresRepository.Load();

            _navigationService = _container.Resolve<INavigationService>();
            _commandObserver = _container.Resolve<IObserveAsync<ICommand>>();
            _commandObserver.Update += Handle;
        }

        private void Handle(object sender, ICommand command)
        {
            ConsoleCommand consoleCommand = (ConsoleCommand)command;

            switch (consoleCommand.Key)
            {
                case ConsoleKey.Escape:
                case ConsoleKey.Enter:
                    _commandObserver.Update -= Handle;
                    _navigationService.Navigate(new HomeView(_container));
                    break;
            }
        }
    }
}
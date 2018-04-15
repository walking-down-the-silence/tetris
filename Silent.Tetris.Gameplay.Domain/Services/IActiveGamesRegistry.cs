using System;
using System.Collections.Generic;
using Silent.Practices.DDD;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Engine;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    public interface IActiveGamesRegistry
    {
        IGameEngine GetActiveGame(Guid gameId);

        void RegisterActiveGame(Guid gameId);

        void RemoveEndedGame(Guid gameId);
    }

    public class ActiveGameRegistry : IActiveGamesRegistry
    {
        private readonly Dictionary<Guid, IGameEngine> _activeGames = new Dictionary<Guid, IGameEngine>();
        private readonly IRepository<GameField, Guid> _repository;

        public ActiveGameRegistry(IRepository<GameField, Guid> repository)
        {
            _repository = repository;
        }

        public IGameEngine GetActiveGame(Guid gameId)
        {
            return _activeGames.ContainsKey(gameId)
                ? _activeGames[gameId]
                : throw new ArgumentException("Game is no longer active and is not present in the registry.");
        }

        public void RegisterActiveGame(Guid gameId)
        {
            _activeGames[gameId] = new GameEngine(_repository);
            _activeGames[gameId].Run(gameId);
        }

        public void RemoveEndedGame(Guid gameId)
        {
            _activeGames[gameId].End();
            _activeGames.Remove(gameId);
        }
    }
}
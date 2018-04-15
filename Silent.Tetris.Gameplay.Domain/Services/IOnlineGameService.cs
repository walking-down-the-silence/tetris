using System;
using Silent.Practices.DDD;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Engine;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    public interface IOnlineGameService
    {
        Guid CreateNewGame();

        IGameField GetGame(Guid gameId);

        void MoveTetromino(Guid gameId, MotionDirection direction);

        void RotateTetromino(Guid gameId, RotateDirection direction);
    }

    public class OnlineGameService : IOnlineGameService
    {
        private readonly IRepository<GameField, Guid> _repository;
        private readonly IActiveGamesRegistry _gamesRegistry;

        public OnlineGameService(IRepository<GameField, Guid> repository, IActiveGamesRegistry gamesRegistry)
        {
            _repository = repository;
            _gamesRegistry = gamesRegistry;
        }

        public Guid CreateNewGame()
        {
            Size gameFieldDefaultSize = new Size(10, 22);
            IGameField gameField = new GameField(gameFieldDefaultSize);

            Guid gameId = gameField.Id;
            _repository.Add(gameField as GameField);
            _gamesRegistry.RegisterActiveGame(gameId);

            return gameId;
        }

        public IGameField GetGame(Guid gameId)
        {
            return _repository.GetById(gameId);
        }

        public void MoveTetromino(Guid gameId, MotionDirection direction)
        {
            IGameField gameField = _repository.GetById(gameId);
            gameField.MoveCurrentFigure(direction);
            _repository.Update(gameField.Id, gameField as GameField);
        }

        public void RotateTetromino(Guid gameId, RotateDirection direction)
        {
            IGameField gameField = _repository.GetById(gameId);
            gameField.RotateCurrentFigure();
            _repository.Update(gameField.Id, gameField as GameField);
        }
    }
}
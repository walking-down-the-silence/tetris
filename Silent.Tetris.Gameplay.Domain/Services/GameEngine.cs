using System;
using System.Threading.Tasks;
using Silent.Practices.DDD;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class GameEngine : IGameEngine
    {
        private readonly IRepository<GameField, Guid> _repository;
        private GameEngineState _gameEngineState;
        private Disposable _gameEngineDisposable;
        private Guid _gameId;

        public GameEngine(IRepository<GameField, Guid> repository)
        {
            _gameEngineState = GameEngineState.New;
            _repository = repository;
        }

        public GameEngineState State => _gameEngineState;

        public event EventHandler<GameStateEventArgs> StateChanged;

        public IDisposable Run(Guid gameId)
        {
            _gameId = gameId;
            _gameEngineDisposable = new Disposable();
            GenerateMoveDownCommandsAsync(500);
            return _gameEngineDisposable;
        }

        public void Pause()
        {
            _gameEngineDisposable.Dispose();
            _gameEngineState = GameEngineState.Paused;
        }

        public void Resume()
        {
            GenerateMoveDownCommandsAsync(500);
            _gameEngineState = GameEngineState.Running;
        }

        public void End()
        {
            _gameEngineDisposable.Dispose();
            _gameEngineState = GameEngineState.Ended;
        }

        public bool IsGameOver()
        {
            IGameField gameField = _repository.GetById(_gameId);

            if (gameField.Ground.Size.Height >= gameField.Size.Height)
            {
                int y = gameField.Size.Height - 1;

                for (int i = 0; i < gameField.Size.Width; i++)
                {
                    if(gameField.Ground[i, y] != Color.Transparent)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected void OnStateChanged()
        {
            // TODO: fix the parameters for this event call
            GameStateEventArgs eventArgs = new GameStateEventArgs(null, null, 0);
            StateChanged?.Invoke(this, eventArgs);
        }
        
        private void GenerateMoveDownCommandsAsync(int delay)
        {
            Task.Run(async () =>
            {
                while (!_gameEngineDisposable.IsDisposed && !IsGameOver())
                {
                    IGameField gameField = _repository.GetById(_gameId);
                    gameField.MoveCurrentFigure(MotionDirection.Down);
                    
                    if (IsGameOver())
                    {
                        OnStateChanged();
                        return;
                    }

                    // TODO: check if figure was merged with gound and if so, generate next figure
                    //bool figureReachedGround = false;
                    //if(figureReachedGround)
                    //{
                    //    gameField.AssignNextFigure(_nextFigure);
                    //    _nextFigure = figureGenerator.GenerateNext();
                    //}

                    OnStateChanged();
                    await Task.Delay(delay);
                }
            });
        }

        private sealed class Disposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}

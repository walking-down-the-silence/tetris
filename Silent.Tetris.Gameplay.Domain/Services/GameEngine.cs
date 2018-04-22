using System;
using System.Threading;
using System.Threading.Tasks;
using Silent.Practices.DDD;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class GameEngine : IGameEngine
    {
        private const int SystemCommandTimeStep = 500;
        private readonly IRepository<GameField, Guid> _repository;
        private CancellationTokenSource _tokenSource;
        private GameEngineState _gameEngineState;
        private Guid _gameId;

        public GameEngine(IRepository<GameField, Guid> repository)
        {
            _gameEngineState = GameEngineState.New;
            _repository = repository;
        }

        public GameEngineState State => _gameEngineState;

        public event EventHandler<GameStateEventArgs> StateChanged;

        public void Run(Guid gameId)
        {
            _gameId = gameId;
            StartGameStepLoop();
        }

        public void Pause()
        {
            StopGameStepLoop(GameEngineState.Paused);
        }

        public void Resume()
        {
            StartGameStepLoop();
        }

        public void End()
        {
            StopGameStepLoop(GameEngineState.Ended);
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

        private void StartGameStepLoop()
        {
            _tokenSource = new CancellationTokenSource();
            GenerateMoveDownCommandsAsync(SystemCommandTimeStep, _tokenSource.Token);
            _gameEngineState = GameEngineState.Running;
        }

        private void StopGameStepLoop(GameEngineState state)
        {
            _tokenSource.Cancel();
            _gameEngineState = state;
        }
        
        private void GenerateMoveDownCommandsAsync(int delay, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested && !IsGameOver())
                {
                    IGameField gameField = _repository.GetById(_gameId);
                    gameField.MoveCurrentFigure(MotionDirection.Down);

                    OnStateChanged();
                    await Task.Delay(delay);
                }
            },
            cancellationToken);
        }
    }
}

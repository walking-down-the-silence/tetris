using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class GameState : IGameState
    {
        public IFigure NextFigure { get; private set; }

        public int CurrentScore { get; private set; }

        public void AssignNextFigure(IFigure nextFigure)
        {
            NextFigure = nextFigure;
        }

        public void SetScore(int currentScore)
        {
            CurrentScore = currentScore;
        }
    }
}
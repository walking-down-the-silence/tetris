using System;

namespace Silent.Tetris.Contracts.Core
{
    public class GameStateEventArgs : EventArgs
    {
        public GameStateEventArgs(IFigure currentFigure, IFigure nextFigure, int currentScore)
        {
            CurrentFigure = currentFigure;
            NextFigure = nextFigure;
            CurrentScore = currentScore;
        }

        public IFigure CurrentFigure { get; private set; }

        public IFigure NextFigure { get; private set; }

        public int CurrentScore { get; private set; }
    }
}
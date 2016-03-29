using System;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Contracts.Core
{
    public interface IGameEngine
    {
        event EventHandler<GameStateEventArgs> StateChanged;

        IDisposable Run(IGameField gameField);
    }
}
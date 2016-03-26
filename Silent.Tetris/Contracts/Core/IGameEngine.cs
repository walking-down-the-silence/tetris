using System;

namespace Silent.Tetris.Contracts.Core
{
    public interface IGameEngine
    {
        IDisposable Run(IGameField gameField);
    }
}
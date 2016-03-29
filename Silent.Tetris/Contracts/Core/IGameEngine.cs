using System;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Contracts.Core
{
    public interface IGameEngine
    {
        IDisposable Run(IGameField gameField);
    }
}
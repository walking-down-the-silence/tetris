using Silent.Tetris.Contracts;
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Views;
using System;

namespace Silent.Tetris
{
    public class NavigationService : INavigationService
    {
        public IView CurrentView { get; private set; }

        public void Navigate(IView view)
        {
            CurrentView = view;
            CurrentView?.Initialize();
            Initialize(CurrentView?.Size);
        }

        private void Initialize(Size viewSize)
        {
            if (viewSize != null && viewSize.Width > 0 && viewSize.Height > 0)
            {
                if (viewSize.Width * 2 != Console.WindowWidth || viewSize.Height != Console.WindowHeight)
                {
                    Console.SetWindowSize(viewSize.Width * 2, viewSize.Height);
                    Console.SetBufferSize(viewSize.Width * 2, viewSize.Height);
                }
            }
        }
    }
}

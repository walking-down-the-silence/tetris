using System.Collections.Generic;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Engine
{
    public class MotionDetector
    {
        public HashSet<MotionDirection> DetectAllowedMotion(IGround ground, IFigure figure)
        {
            HashSet<MotionDirection> allowedMovement = new HashSet<MotionDirection>();
            bool canShiftLeft = true;
            bool canShiftRight = true;
            bool canShiftDown = true;

            for (int x = 0; x < figure.Size.Width; x++)
            {
                for (int y = 0; y < figure.Size.Height; y++)
                {
                    if (figure[x, y] != Color.Transparent)
                    {
                        canShiftLeft = canShiftLeft && IsShiftAllowed(ground, figure, x, y, -1, 0);
                        canShiftRight = canShiftRight && IsShiftAllowed(ground, figure, x, y, 1, 0);
                        canShiftDown = canShiftDown && IsShiftAllowed(ground, figure, x, y, 0, -1);
                    }
                }
            }

            if (canShiftLeft) allowedMovement.Add(MotionDirection.Left);
            if (canShiftRight) allowedMovement.Add(MotionDirection.Right);
            if (canShiftDown) allowedMovement.Add(MotionDirection.Down);

            return allowedMovement;
        }

        private bool IsShiftAllowed(IGround ground, IFigure figure, int x, int y, int xShift, int yShift)
        {
            bool shiftAllowed = false;
            int figureLeft = figure.Position.Left + x + xShift;
            int figureTop = figure.Position.Bottom + y + yShift;

            int groundX = figureLeft - ground.Position.Left;
            int groundY = figureTop - ground.Position.Bottom;

            if (groundY >= 0 &&
                groundY < ground.Size.Height &&
                groundX >= 0 &&
                groundX < ground.Size.Width &&
                ground[groundX, groundY] == Color.Transparent &&
                IsFigureInGameFieldBounds(ground, figure))
            {
                shiftAllowed = true;
            }

            return shiftAllowed;
        }

        private bool IsFigureInGameFieldBounds(IGround ground, ISprite sprite)
        {
            return sprite.Position.Bottom >= ground.Position.Bottom
                && sprite.Position.Left + sprite.Size.Width < ground.Position.Left + ground.Size.Width + 1
                && sprite.Position.Left >= ground.Position.Left;
        }
    }
}
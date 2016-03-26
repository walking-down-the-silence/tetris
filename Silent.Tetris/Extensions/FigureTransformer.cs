using System;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Extensions
{
    public static class SpriteTransformer
    {
        public static IFigure Move(this IFigure figure, MotionDirection motionDirection)
        {
            Position newPosition;

            switch (motionDirection)
            {
                case MotionDirection.Left:
                    newPosition = new Position(figure.Position.Left - 1, figure.Position.Bottom);
                    break;
                case MotionDirection.Right:
                    newPosition = new Position(figure.Position.Left + 1, figure.Position.Bottom);
                    break;
                case MotionDirection.Down:
                    newPosition = new Position(figure.Position.Left, figure.Position.Bottom - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(motionDirection), motionDirection, null);
            }

            IFigure clonedFigure = figure.SetPosition(newPosition);
            return clonedFigure;
        }
    }
}
using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Contracts.Rendering;

namespace Silent.Tetris.Core.Renderers
{
    public class GameFieldRenderer : IGameFieldRenderable
    {
        public void Render(IGameField gameField)
        {
            ISpriteRenderable spriteRenderer = new SpriteRenderer();
            spriteRenderer.Render(gameField.Ground);
            spriteRenderer.Render(gameField.CurrentFigure);
            spriteRenderer.Render(gameField.NextFigure);

            int nextFIgureFieldX = gameField.Ground.Position.Left + gameField.Ground.Size.Width + 1;
            int nextFIgureFieldY = gameField.Ground.Position.Bottom + gameField.Ground.Size.Height / 2 - 1;
            ConsoleHelper.WriteAtPosition(nextFIgureFieldX, nextFIgureFieldY, "Next Figure");

            ConsoleHelper.WriteAtPosition(nextFIgureFieldX, nextFIgureFieldY - 2, "Score:");
            ConsoleHelper.WriteAtPosition(nextFIgureFieldX, nextFIgureFieldY - 3, "179000");
        }
    }
}
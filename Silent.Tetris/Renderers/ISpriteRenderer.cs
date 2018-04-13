using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Rendering
{
    /// <summary>
    /// Represents the type that is able to render <see cref="ISprite"/> objects.
    /// </summary>
    public interface ISpriteRenderer : IRenderer<ISprite>
    {
    }
}
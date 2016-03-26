namespace Silent.Tetris.Contracts.Rendering
{
    public interface IRenderable<in TSource>
    {
        void Render(TSource source);
    }
}
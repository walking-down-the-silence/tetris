namespace Silent.Tetris.Contracts.Rendering
{
    /// <summary>
    /// Represents the type that is able to render objects.
    /// </summary>
    /// <typeparam name="TSource"> The <see cref="TSource"/> type. </typeparam>
    public interface IRenderer<in TSource>
    {
        /// <summary>
        /// Performs rendering of a <see cref="TSource"/> object instance.
        /// </summary>
        /// <param name="source"></param>
        void Render(TSource source);
    }
}
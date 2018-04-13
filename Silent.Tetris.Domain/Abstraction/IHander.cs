namespace Silent.Tetris.Contracts.Handlers
{
    /// <summary>
    /// The object handler.
    /// </summary>
    /// <typeparam name="TSource"> The <see cref="TSource"/> type. </typeparam>
    public interface IHander<in TSource>
    {
        /// <summary>
        /// Handles the <see cref="TSource"/> object instance.
        /// </summary>
        /// <param name="source"> The <see cref="TSource"/> object instance. </param>
        void Handle(TSource source);
    }
}
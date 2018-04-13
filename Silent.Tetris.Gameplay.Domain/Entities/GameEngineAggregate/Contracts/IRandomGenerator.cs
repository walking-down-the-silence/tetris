namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the random object generator from a certain set of objects.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IRandomGenerator<out TSource>
    {
        /// <summary>
        /// Generates the next random object.
        /// </summary>
        /// <returns> The <see cref="TSource"/> instance. </returns>
        TSource GenerateNext();
    }
}
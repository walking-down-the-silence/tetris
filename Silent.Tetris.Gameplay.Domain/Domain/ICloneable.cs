namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the object with an ability to clone itself.
    /// </summary>
    /// <typeparam name="TOutput"> The cloned instance of <see cref="TOutput"/>. </typeparam>
    public interface ICloneable<out TOutput>
    {
        /// <summary>
        /// Clones the object.
        /// </summary>
        /// <param name="parameters"> The parameters used for clone object. </param>
        /// <returns> Cloned instance. </returns>
        TOutput Clone(params object[] parameters);
    }
}
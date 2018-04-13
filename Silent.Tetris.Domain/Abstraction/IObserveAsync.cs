using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Observes some action and notifies the client.
    /// </summary>
    /// <typeparam name="TOuput"></typeparam>
    public interface IObserveAsync<TOuput>
    {
        /// <summary>
        /// Update event to notify about changes.
        /// </summary>
        event EventHandler<TOuput> Update;

        /// <summary>
        /// Performs observation over a certain object.
        /// </summary>
        /// <returns> The <see cref="IDisposable"/> instance that stops observing. </returns>
        IDisposable ObserveAsync();
    }
}
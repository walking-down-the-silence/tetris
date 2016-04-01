using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents a message bus.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    public interface IMessageBus<TSource> : IDisposable
    {
        /// <summary>
        /// Adds a message to queue.
        /// </summary>
        /// <param name="source"> The <see cref="TSource"/> instance. </param>
        void Add(TSource source);

        /// <summary>
        /// Gets the message from queue.
        /// </summary>
        /// <returns></returns>
        TSource Take();

        /// <summary>
        /// Completed adding of new messages.
        /// </summary>
        void Complete();
    }
}

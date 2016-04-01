using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// REpresents the container for resolving dependencies.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Resolves requested service.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <returns> The <see cref="TService"/> implementation instance. </returns>
        TService Resolve<TService>() where TService : class;

        /// <summary>
        /// Resolves requested service using name.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <param name="serviceName"> The service name. </param>
        /// <returns> The <see cref="TService"/> implementation instance. </returns>
        TService Resolve<TService>(string serviceName) where TService : class;

        /// <summary>
        /// Registers the implementation of a service.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <param name="serviceInstance"> An instance of a service. </param>
        void Register<TService>(object serviceInstance) where TService : class;

        /// <summary>
        /// Registers the implementation of a service.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <param name="serviceName"> The service name. </param>
        /// <param name="serviceInstance"> An instance of a service. </param>
        void Register<TService>(string serviceName, object serviceInstance) where TService : class;

        /// <summary>
        /// Registers the implementation of a service.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <param name="factoryMethod"> A factory method to create an instance. </param>
        void Register<TService>(Func<IContainer, object> factoryMethod) where TService : class;

        /// <summary>
        /// Registers the implementation of a service.
        /// </summary>
        /// <typeparam name="TService"> The type of service. </typeparam>
        /// <param name="serviceName"> The service name. </param>
        /// <param name="factoryMethod"> A factory method to create an instance. </param>
        void Register<TService>(string serviceName, Func<IContainer, object> factoryMethod) where TService : class;
    }
}
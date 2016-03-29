using System;

namespace Silent.Tetris.Contracts
{
    public interface IContainer
    {
        TService Resolve<TService>() where TService : class;

        TService Resolve<TService>(string serviceName) where TService : class;

        void Register<TService>(object serviceInstance) where TService : class;

        void Register<TService>(string serviceName, object serviceInstance) where TService : class;

        void Register<TService>(Func<IContainer, object> factoryMethod) where TService : class;

        void Register<TService>(string serviceName, Func<IContainer, object> factoryMethod) where TService : class;
    }
}
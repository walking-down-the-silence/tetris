using System;

namespace Silent.Tetris.Contracts
{
    public interface IContainer
    {
        TService Resolve<TService>() where TService : class;

        TService Resolve<TService>(string serviceName) where TService : class;

        void Register<TService>(TService serviceInstance) where TService : class;

        void Register<TService>(string serviceName, TService serviceInstance) where TService : class;

        void Register<TService>(Func<TService> factoryMethod) where TService : class;

        void Register<TService>(string serviceName, Func<TService> factoryMethod) where TService : class;
    }
}
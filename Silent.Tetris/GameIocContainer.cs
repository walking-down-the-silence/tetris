using System;
using System.Collections.Generic;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class GameIocContainer : IContainer
    {
        private readonly IDictionary<Tuple<Type, string>, object> _registeredFactories = new Dictionary<Tuple<Type, string>, object>();

        public TService Resolve<TService>() where TService : class
        {
            return Resolve<TService>(string.Empty);
        }

        public TService Resolve<TService>(string serviceName) where TService : class
        {
            Tuple<Type, string> key = new Tuple<Type, string>(typeof(TService), serviceName);
            if (_registeredFactories.ContainsKey(key))
            {
                string exceptionMessage = string.IsNullOrWhiteSpace(serviceName)
                    ? $"Service of type '{typeof(TService)}' is not registered"
                    : $"Service of type '{typeof(TService)}' with name '{serviceName}' is is not registered";

                throw new ArgumentException(exceptionMessage);
            }

            return _registeredFactories[key] as TService;
        }

        public void Register<TService>(TService serviceInstance) where TService : class
        {
            Register(string.Empty, serviceInstance);
        }

        public void Register<TService>(string serviceName, TService serviceInstance) where TService : class
        {
            Register(serviceName, () => serviceInstance);
        }

        public void Register<TService>(Func<TService> factoryMethod) where TService : class
        {
            Register(string.Empty, factoryMethod);
        }

        public void Register<TService>(string serviceName, Func<TService> factoryMethod) where TService : class
        {
            Tuple<Type, string> key = new Tuple<Type, string>(typeof(TService), serviceName);
            if (_registeredFactories.ContainsKey(key))
            {
                string exceptionMessage = string.IsNullOrWhiteSpace(serviceName)
                    ? $"Service of type '{typeof(TService)}' is already registered"
                    : $"Service of type '{typeof(TService)}' with name '{serviceName}' is already registered";

                throw new ArgumentException(exceptionMessage);
            }

            _registeredFactories.Add(key, factoryMethod);
        }
    }
}
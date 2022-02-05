
using System;
using System.Collections.Generic;

namespace BasicWebServer.Server.Common
{
    public class ServiceCollection : IServiceCollection
    {
        private readonly Dictionary<Type, Type> services;
        public ServiceCollection()
        {
            services = new Dictionary<Type, Type>();
        }

        public IServiceCollection Add<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            services[typeof(TService)] = typeof(TImplementation);

            return this;
        }

        public IServiceCollection Add<TService>() where TService : class
        {
            throw new NotImplementedException();
        }

        public object CreateInstance(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public TService Get<TService>() where TService : class
        {
            throw new NotImplementedException();
        }
    }
}

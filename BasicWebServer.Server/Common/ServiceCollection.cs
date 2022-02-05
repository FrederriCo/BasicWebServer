
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
            return Add<TService, TService>();
        }

        public object CreateInstance(Type serviceType)
        {
            if (services.ContainsKey(serviceType))
            {
                serviceType = services[serviceType];
            }
            else if (serviceType.IsInterface)
            {
                throw new InvalidOperationException($"Service {serviceType.FullName} is not registred");
            }

            var constrconstructors = serviceType.GetConstructors();

            if (constrconstructors.Length > 1)
            {
                throw new InvalidOperationException("Multiple constructors are not supported");
            }
           
        }

        public TService Get<TService>() where TService : class
        {
            
        }
    }
}

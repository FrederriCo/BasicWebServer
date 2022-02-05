
using System;
using System.Collections.Generic;
using System.Linq;

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

            var constructors = serviceType.GetConstructors();

            if (constructors.Length > 1)
            {
                throw new InvalidOperationException("Multiple constructors are not supported");
            }

            var constructor = constructors.First();
            var parametars = constructor.GetParameters();
            var parametarValues = new object[parametars.Length];

            for (int i = 0; i < parametarValues.Length; i++)
            {
                var parametarType = parametars[i].ParameterType;
                var parametarValue = CreateInstance(parametarType);

                parametarValues[i] = parametarValues;
            }

            return constructor.Invoke(parametarValues);
           
        }

        public TService Get<TService>() where TService : class
        {
            var serviceType = typeof(TService);

            if (!services.ContainsKey(serviceType))
            {
                return null;
            }

            var service = services[serviceType];

            return (TService)CreateInstance(service);
        }
    }
}

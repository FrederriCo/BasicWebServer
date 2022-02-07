using BasicWebServer.Server.Attributies;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicWebServer.Server.Routing
{
    public static class RoutingTableExtensions
    {
        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction) where TController : Controller
            => routingTable.MapGet(
                path, 
                request
                => controllerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(
            this IRoutingTable routingTable,
            string path,
            Func<TController, Response> controllerFunction)
            where TController : Controller
            => routingTable.Map(
            Method.Post,
                path, 
                request 
                => controllerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapControllers(this IRoutingTable routingTable)
        {
            IEnumerable<MethodInfo> controllerActions = GetControllerActions();

            foreach (var controlerAction in controllerActions)
            {
                string controllerName = controlerAction
                    .DeclaringType
                    .Name
                    .Replace(nameof(Controller), string.Empty);

                string actionName = controlerAction.Name;
                string path = $"{controllerName}/{actionName}";

                var responseFunction = GetResponseFunction(controlerAction);

                Method httpMethod = Method.Get;
                var actionMethodAttribute = controlerAction
                    .GetCustomAttribute<HttpMethodAttribute>();

                if (actionMethodAttribute != null)
                {
                    httpMethod = actionMethodAttribute.HttpMethod;
                }

                routingTable.Map(httpMethod, path, responseFunction);
            }

            return routingTable;
        }

        private static Func<Request, Response> GetResponseFunction(MethodInfo controllerAction)
        {
            return request =>
            {
                var controllerInstance = CreateController(controllerAction.DeclaringType, request);
                var parameterValues = GetParameterValues(controllerAction, request);

                return (Response)controllerAction.Invoke(controllerInstance, parameterValues);
            };
          
        }

        private static object[] GetParameterValues(MethodInfo controllerAction, Request request)
        {
            var actionParamaters = controllerAction
                    .GetParameters()
                    .Select(p => new
                    {
                        p.Name,
                        p.ParameterType
                    })
                    .ToArray();

            var parameterValues = new object[actionParamaters.Count()];

            for (int i = 0; i < actionParamaters.Length; i++)
            {
                var parameter = actionParamaters[i];

                if (parameter.ParameterType.IsPrimitive
                    || parameter.ParameterType == typeof(string))
                {
                    var parameterValue = request.GetValue(parameter.Name);
                }
                else
                {

                }
            }
        }

        private static IEnumerable<MethodInfo> GetControllerActions()
             => Assembly
            .GetEntryAssembly()
            .GetExportedTypes()
            .Where(t => t.IsAbstract == false)
            .Where(t => t.IsAssignableTo(typeof(Controller)))
            .Where(t => t.Name.EndsWith(nameof(Controller)))
            .SelectMany(t => t
                 .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                 .Where(m => m.ReturnType.IsAssignableTo(typeof(Response))))
            .ToList();
            
            

        private static TController CreateController<TController>(Request request)
            => (TController)Activator
            .CreateInstance(typeof(TController), new[] { request });

        private static Controller CreateController(Type controllerType, Request request)
        {
            var controller = (Controller)Request.ServiceCollection.CreateInstance(controllerType);

            controllerType
                .GetProperty("Request", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controller, request);

            return controller; 
        }
    }
}

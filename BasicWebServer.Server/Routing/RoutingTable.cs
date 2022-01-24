
using BasicWebServer.Server.HTTP.Request;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;

namespace BasicWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<Method, Dictionary<string, Response>> routes;

        public RoutingTable() =>
                this.routes = new Dictionary<Method, Dictionary<string, Response>>()
                {
                    [Method.Get] = new Dictionary<string, Response>(),
                    [Method.Post] = new Dictionary<string, Response>(),
                    [Method.Put] = new Dictionary<string, Response>(),
                    [Method.Delete] = new Dictionary<string, Response>()

                };
        public IRoutingTable Map(string url, Method method, Response response)
            => method switch
            {
                Method.Get => this.MapGet(url, response),
                Method.Post => this.MapPost(url, response),
                _ => throw new InvalidOperationException(
                    $"Method '{method}' is not supported.")
            };

        public IRoutingTable MapGet(string url, Response response)
        {
            throw new System.NotImplementedException();
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            throw new System.NotImplementedException();
        }
    }
}

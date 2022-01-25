﻿
using BasicWebServer.Server.Common;
using BasicWebServer.Server.HTTP;
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
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this.routes[Method.Get][url] = response;

            return this;
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response  , nameof(response));

            this.routes[Method.Post][url] = response;

            return this;
        }

        public Response MatchRequest(Request request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this.routes.ContainsKey(requestMethod) ||
                !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return this.routes[requestMethod][requestUrl];
        }
    }
}

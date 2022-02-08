using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using System;

namespace BasicWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        //IRoutingTable Map(string url, Method method, Response response);
        //IRoutingTable MapGet(string url, Response response);
        //IRoutingTable MapPost(string url, Response response);

        IRoutingTable Map(Method method, string path,
            Func<Request, Response> responseFunction);

        //IRoutingTable MapGet(string path, Func<Request, Response> responseFunction);

        //IRoutingTable MapPost(string path, Func<Request, Response> responseFunction);
    }
}

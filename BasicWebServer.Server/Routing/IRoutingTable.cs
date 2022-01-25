using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;

namespace BasicWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(string url, Method method, Response response);
        IRoutingTable MapGet(string url, Response response);
        IRoutingTable MapPost(string url, Response response);
    }
}

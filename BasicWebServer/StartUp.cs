using BasicWebServer.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer
{
    public class StartUp
    {
        public static void Main()

           => new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from the server!"))
            .MapGet("/", new HtmlResponse("<h1>HTML response</h1>"))
            .MapGet("/", new RedirectdResponse("https://www.google.bg")))

          .Start();

    }
}

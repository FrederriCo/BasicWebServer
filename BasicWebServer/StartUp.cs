using BasicWebServer.Server;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer
{
    public class StartUp
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
            Name: <input type='text' name='Name' />
            Age: <input type='number' name='Age' />
            <input type='submit' value='Save' />
            </form>";
        public static void Main()

           => new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from the server!"))
            .MapGet("/Redirect", new RedirectResponse("https://www.softuni.org"))
            .MapGet("/Html", new HtmlResponse(StartUp.HtmlForm))
            .MapPost("/HTML", new TextResponse("")))

          .Start();

    }
}

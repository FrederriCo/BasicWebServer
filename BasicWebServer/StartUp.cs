using BasicWebServer.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var server = new HttpServer("127.0.0.1", 8080);
            server.Start();                      


        }
    }
}

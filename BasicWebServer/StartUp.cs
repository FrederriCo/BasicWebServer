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
        {
           var server = new HttpServer("127.0.0.1", 5656);
          server.Start();               


        }
    }
}

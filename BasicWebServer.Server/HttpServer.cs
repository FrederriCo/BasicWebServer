﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        public HttpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            serverListener = new TcpListener(this.ipAddress, port);           
        }

        public void Start()
        {
            this.serverListener.Start();

            Console.WriteLine($"Server started on port {port}.");
            Console.WriteLine("Listening for request.....");

            while (true)
            {
                var connection = serverListener.AcceptTcpClient();

                var networkSteram = connection.GetStream();

                WriteResponse(networkSteram, "Hello from Server");

                connection.Close();
            }
        }

        private void WriteResponse(NetworkStream networkSteram, string message)
        {
            var contentLength = Encoding.UTF8.GetByteCount(message);

            var response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {contentLength}

{message}";
            var responseByte = Encoding.UTF8.GetBytes(response);

            networkSteram.Write(responseByte);            

        }
    }
}
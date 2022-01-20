using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");

            var port = 5656;

            var serverListener = new TcpListener(ipAddress, port);

            serverListener.Start();

            Console.WriteLine($"Server started on port {port}.");
            Console.WriteLine("Listening for request.....");

            while (true)
            {

                var connection = serverListener.AcceptTcpClient();

                var networkStream = connection.GetStream();

                var content = "Hello from Server!";
                var contentLength = Encoding.UTF8.GetByteCount(content);

                var response = $@"HTTP/1.1 200 OK
Content-Type: text/plain; charset=UTF-8
Content-Length: {contentLength}

{content}";
                var responseByte = Encoding.UTF8.GetBytes(response);

                networkStream.Write(responseByte);

                //connection.Close();
            }
        }
    }
}

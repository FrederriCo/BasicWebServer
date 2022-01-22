using BasicWebServer.Server.HTTP.Response;

namespace BasicWebServer.Server.HTTP.Request
{
    public class Request
    {
        public Method Method { get; private set; }
        public string Url { get; private set; }
        public HeaderCollection Headers{ get; private set; }
        public string Body { get; private set; }

    }
}

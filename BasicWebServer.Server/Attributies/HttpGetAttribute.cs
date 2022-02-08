using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributies
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() 
            : base(Method.Get)
        {
        }
    }
}

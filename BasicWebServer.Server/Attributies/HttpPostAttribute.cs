
using BasicWebServer.Server.HTTP;

namespace BasicWebServer.Server.Attributies
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base(Method.Post)
        {
        }
    }
}

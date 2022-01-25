
namespace BasicWebServer.Server.HTTP.Response
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) 
            : base(text, ContentType.html)
        {
        }
    }
}


using System;

namespace BasicWebServer.Server.HTTP.Response
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text,
            Action<Request, Response> preRenderAction = null) 
            : base(text, ContentType.html, preRenderAction)
        {
        }
    }
}

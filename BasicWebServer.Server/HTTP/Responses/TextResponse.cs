
using System;


namespace BasicWebServer.Server.HTTP.Response
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text,
            Action<Request, Response> preRenderAction = null)
            : base(text, ContentType.PlainText, preRenderAction)
        {
        }
    }
}


using System;


namespace BasicWebServer.Server.HTTP.Response
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)            
            : base(text, ContentType.PlainText)
        {
        }
    }
}

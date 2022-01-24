namespace BasicWebServer.Server.HTTP.Response
{
    public class RedirectResponse : Response
    {
        public RedirectResponse(string location) 
            : base(StatusCode.Found)
        {
            this.Headers.Add(Header.Location, location);
        }
    }
}

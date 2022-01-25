using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer
{
    public class StartUp
    {
        private const string HtmlForm = @"<form action='/HTML' method='POST'>
            Name: <input type='text' name='Name' />
            Age: <input type='number' name='Age' />
            <input type='submit' value='Save' />
            </form>";

        private const string DownloadForm = @"<form action='/Content' method='POST'>
            <input type='submit' value='Download Sites Content' />
            </form>";

        private const string FileName = "content.txt";
        public static async Task Main()

           => await new HttpServer(routes => routes
            .MapGet("/", new TextResponse("Hello from the server!"))
            .MapGet("/Redirect", new RedirectResponse("https://www.softuni.org"))
            .MapGet("/Html", new HtmlResponse(StartUp.HtmlForm))
            .MapPost("/HTML", new TextResponse("", StartUp.AddFormDataAction))
            .MapGet("/Content", new HtmlResponse(StartUp.DownloadForm))
            .MapPost("/Content", new TextFileResponse(StartUp.FileName)))

          .Start();

        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();
        }
        private static async Task<string> DownloadWebSiteContent(string url)
        {
            var httpClient = new HttpClient();

            using (httpClient)
            {
                var response = await httpClient.GetAsync(url);

                var html = await response.Content.ReadAsStringAsync();

                return html.Substring(0, 2000);
            }
        }
        private static void AddFormDataAction(Request request, Response response)
        {
            response.Body = "";

            foreach (var (key, value) in request.Form)
            {
                response.Body += $"{key} - {value}";
                response.Body += Environment.NewLine;
            }
        }
    }
}

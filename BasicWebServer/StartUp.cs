using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        {
            await DownloadSitesAsTextFile(StartUp.FileName,
                new string[] { "https://www.yahoo.com", "https://www.dir.bg" });

            var server = new HttpServer(routes => routes
           .MapGet("/", new TextResponse("Hello from the server!"))
           .MapGet("/Redirect", new RedirectResponse("https://www.softuni.org"))
           .MapGet("/Html", new HtmlResponse(StartUp.HtmlForm))
           .MapPost("/HTML", new TextResponse("", StartUp.AddFormDataAction))
           .MapGet("/Content", new HtmlResponse(StartUp.DownloadForm))
           .MapPost("/Content", new TextFileResponse(StartUp.FileName))
           .MapGet("/Cookies", new HtmlResponse("<h1>Coockies Sets!</h1>", StartUp.AddCookieAction)));

           await server.Start();
        }

        private static void AddCookieAction(Request request, Response response)
        {
            var requestHasCoookies = request.Cookies.Any(c => c.Name != Session.SessionCookieName);
            var bodyText = "";

            if (requestHasCoookies)
            {
                var cookieText = new StringBuilder();
                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText
                    .Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText.
                        Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText.
                        Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</tr>");
                }

                cookieText.Append("</table>");

                bodyText = cookieText.ToString();
            }
            else
            {
                bodyText = "<h1>Cookie set!</h1>";
            }

            response.Body = "";
            response.Body += bodyText;

            if (!requestHasCoookies)
            {
                response.Cookies.Add("My-Cookie", "My-Value");
                response.Cookies.Add("My-Second-Cookie", "My-Second-Value");
            }
        }
        private static async Task DownloadSitesAsTextFile(string fileName, string[] urls)
        {
            var downloads = new List<Task<string>>();

            foreach (var url in urls)
            {
                downloads.Add(DownloadWebSiteContent(url));
            }

            var responses = await Task.WhenAll(downloads);

            var responseString = string.Join(Environment.NewLine +
                new String('-', 100), responses);

            await File.WriteAllTextAsync(fileName, responseString);
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

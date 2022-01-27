using BasicWebServer.Controllers;
using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using BasicWebServer.Server.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicWebServer
{
    public class StartUp
    {
        private const string LoginForm = @"<form action='/Login' method='POST'>
            Username: <input type='text' name='Username'/>
            Password: <input type='text' name='Password'/>
                <input type='submit' value='Log In'>
            </form>";

        private const string Username = "user";
        private const string Password = "user123";
        public static async Task Main()
        {
            //await DownloadSitesAsTextFile(StartUp.FileName,
            //    new string[] { "https://www.yahoo.com", "https://www.dir.bg" });

            var server = new HttpServer(routes => routes
           .MapGet<HomeController>("/", c => c.Index())
           .MapGet<HomeController>("/Redirect", c => c.Redirect())
           .MapGet<HomeController>("/Html", c => c.Html())
           .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
           .MapGet<HomeController>("/Content", c => c.Content())
           .MapPost<HomeController>("/Content", c => c.DownloadContent())
           .MapGet<HomeController>("/Cookies", c => c.Cookies())
           .MapGet<HomeController>("/Session", c => c.Session())));
            //.MapGet("/Login", new HtmlResponse(StartUp.LoginForm))
            //.MapPost("/Login", new HtmlResponse("", StartUp.LoginAction))
            //.MapGet("/Logout", new HtmlResponse("", StartUp.LogoutAction))
            //.MapGet("/UserProfile", new HtmlResponse("", StartUp.GetUserDataAction)));

            await server.Start();
        }

        private static void GetUserDataAction(Request request, Response response)
        {
            if (request.Session.ContainsKey(Session.SessionUserKey))
            {
                response.Body = "";
                response.Body += $"<h3>Currently Logged-in user " +
                    $"is with username '{Username}'</h3>";
            }
            else
            {
                response.Body = "";
                response.Body += $"<h3>You should first log in " +
                    "- <a href='/Login'>Login</a><h3>";
            }
        }

        private static void LogoutAction(Request request, Response response)
        {
            request.Session.Clear();

            response.Body = "";
            response.Body += "<h3>Logged out successfully!</h3>";
        }

        private static void LoginAction(Request request, Response response)
        {
            request.Session.Clear();

            var bodyText = "";

            var usernameMatches = request.Form["Username"] == StartUp.Username;
            var passwordMatches = request.Form["Password"] == StartUp.Password;

            if (usernameMatches && passwordMatches)
            {
                request.Session[Session.SessionUserKey] = "MyUserId";
                response.Cookies.Add(Session.SessionCookieName, request.Session.Id);

                bodyText = "<h3>Logged successfully!</h3>";
            }
            else
            {
                bodyText = StartUp.LoginForm;
            }

            response.Body = "";
            response.Body += bodyText;
        }

        private static void DisplaySessionInfoAction(Request request, Response response)
        {
            var sessionExists = request.Session.ContainsKey(Session.SessionCurrentDateKey);

            var bodyText = "";

            if (sessionExists)
            {
                var curnetDate = request.Session[Session.SessionCurrentDateKey];
                bodyText = $"Stored date: {curnetDate}!";
            }
            else
            {
                bodyText = "Current date stored;";
            }

            response.Body = "";
            response.Body += bodyText;

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

using BasicWebServer.Models;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicWebServer.Controllers
{
    public  class HomeController : Controller
    {
        private const string FileName = "content.txt";

        public HomeController(Request request)
            : base(request)
        {

        }
        public Response Index() => Text("Hello from server!");

        public Response Html() => View();

        public Response Redirect() => Redirect("https://softuni.bg");

        public Response Content() => View();

       // public Response Html() => Html(HomeController.HtmlForm);

        public Response Login() => View();

        public Response HtmlFormPost()
        {
            var name = Request.Form["Name"];
            var age = Request.Form["Age"];

            var model = new FormViewModel()
            {
                Name = name,
                Age = int.Parse(age)
            };

            return View(model);
        }

        public Response DownloadContent()
        {
            DownloadSitesAsTextFile(HomeController.FileName,
               new string[] {  "https://www.yahoo.com", "https://www.dir.bg"})
             .Wait();

            return File(HomeController.FileName);
        }

        public Response Cookies()
        {
            if (Request.Cookies.Any(
                c => c.Name != BasicWebServer.Server.HTTP.Session.SessionCookieName))
            {
                var cookieText = new StringBuilder();
                cookieText.AppendLine("<h1>Cookies</h1>");

                cookieText
                    .Append("<table border='1'><tr><th>Name</th><th>Value</th></tr>");

                foreach (var cookie in Request.Cookies)
                {
                    cookieText.Append("<tr>");
                    cookieText.
                        Append($"<td>{HttpUtility.HtmlEncode(cookie.Name)}</td>");
                    cookieText.
                        Append($"<td>{HttpUtility.HtmlEncode(cookie.Value)}</td>");
                    cookieText.Append("</tr>");
                }


                cookieText.Append("</table>");

                return Html(cookieText.ToString());
            }

            var cookies = new CookieCollection();

           cookies.Add("My-Cookie", "My-Value");
            cookies.Add("My-Second-Cookie", "My-Second-Value");

            return Html("<h1>Cookie set!</h1>");                   
                         
        }

        public Response Session()
        {
            string currentDateKey = "CurrentDate";
            var sessionExists = Request.Session.ContainsKey(currentDateKey);                      

            if (sessionExists)
            {
                var curnetDate = Request.Session[currentDateKey];

                return Text($"Stored date: {curnetDate}!");
            }

            return Text("Current date stored!");
            
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
            
         await System.IO.File.WriteAllTextAsync(fileName, responseString);
            
                      
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
    }

}

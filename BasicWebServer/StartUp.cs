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
        public static async Task Main()
        {
            // await new HttpServer(routes => routes
            //.MapGet<HomeController>("/", c => c.Index())
            //.MapGet<HomeController>("/Redirect", c => c.Redirect())
            //.MapGet<HomeController>("/Html", c => c.Html())
            //.MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
            //.MapGet<HomeController>("/Content", c => c.Content())
            //.MapPost<HomeController>("/Content", c => c.DownloadContent())
            //.MapGet<HomeController>("/Cookies", c => c.Cookies())
            //.MapGet<HomeController>("/Session", c => c.Session())
            //.MapGet<UsersController>("/Login", u => u.Login())
            //.MapPost<UsersController>("/Login", u => u.LogInUser())
            //.MapGet<UsersController>("/Logout", u => u.Logout())
            //.MapGet<UsersController>("/UserProfile", u => u.GetUserData()))
            //.Start();

            await new HttpServer(routers => routers
                .MapControllers())
                .Start();
        }    
    }
}

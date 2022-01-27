
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.Response;

namespace BasicWebServer.Controllers
{
    public class UsersController : Controller
    {
        private const string LoginForm = @"<form action='/Login' method='POST'>
            Username: <input type='text' name='Username'/>
            Password: <input type='text' name='Password'/>
                <input type='submit' value='Log In'>
            </form>";

        private const string Username = "user";
        private const string Password = "user123";

        public UsersController(Request request)
            : base(request)
        {
        }

        public Response Login() => View();

        public Response LogInUser()
        {
            Request.Session.Clear();           

            var usernameMatches = Request.Form["Username"] == UsersController.Username;
            var passwordMatches = Request.Form["Password"] == UsersController.Password;

            if (usernameMatches && passwordMatches)
            {
                if (Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, Request.Session.Id);

                    return Html("<h3> Logged successfully! </h3>", cookies);
                }

                return Html("<h3> Logged successfully! </h3>");
            }

            return Redirect("/Login");           
        }

        public Response Logout()
        {
            Request.Session.Clear();
                 
            return Html("<h3>Logged out successfully!</h3>");
        }

        public Response GetUserData()
        {
            if (this.Request.Session.ContainsKey(Session.SessionUserKey))
            {
               
                return Html( $"<h3>Currently Logged-in user is " +
                    $"with username '{UsersController.Username}'</h3>");
            }

            return Redirect("/Login");
        }
    }
}

using Sdl.Community.GroupShareKit.Authentication;
using Sdl.Community.GroupShareKit.Helpers;

namespace Sdl.Community.GroupShareKit.Http
{
    public class Credentials
    {

        public Credentials(string token)
        {
            Ensure.ArgumentNotNullOrEmptyString(token, "token");

            Login = null;
            Password = token;
            AuthenticationType = AuthenticationType.Oauth;
        }
        public Credentials(string login, string password)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");
            Ensure.ArgumentNotNullOrEmptyString(password, "password");

            Login = login;
            Password = password;
            AuthenticationType = AuthenticationType.Basic;
        }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public AuthenticationType AuthenticationType
        {
            get;
            private set;
        }

        public string GetToken()
        {
            return Password;
        }
    }
}
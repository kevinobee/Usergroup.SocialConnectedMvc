using System.Web.Mvc;
using Website.Abstractions.Security.Authentication;
using Website.Abstractions.SocialConnected;

namespace Website.Controllers
{
    public class SocialConnectedAuthenticationController : Controller
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IConnectUserManager _connectUserManager;

        public SocialConnectedAuthenticationController(
            IAuthenticationManager authenticationManager,
            IConnectUserManager connectUserManager)
        {
            _authenticationManager = authenticationManager;
            _connectUserManager = connectUserManager;
        }

        // GET: /login/{networkName}
        public ActionResult Login(string networkName)
        {
            if (!_authenticationManager.GetActiveUser().IsAuthenticated)
                _connectUserManager.LoginUser(networkName, true, null);
            else
                _connectUserManager.AttachUser(networkName, true, null);

            return new EmptyResult();
        }

        // GET: /logout
        public ActionResult Logout()
        {
            _authenticationManager.Logout();
            return new RedirectResult("/");
        }
    }
}

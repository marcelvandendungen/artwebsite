using System.Web;
using Core.Model;
using System.Web.Mvc;

namespace Core.Interface
{
    public interface IMembershipService
    {
        //IAuthenticationRequest CreateAuthenticationRequest(string identifier);
        ActionResult RedirectToAuthenticationProvider(string authIdentifier);
        HttpCookie CreateFormsAuthenticationCookie(User user);
        User GetUser();
        void SignOut();

    }
}

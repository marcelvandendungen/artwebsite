using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IlseLeijten
{
    public class AuthorizeUsers : AuthorizeAttribute
    {
        private readonly string[] _allowedUsers;

        public AuthorizeUsers()
        {
            _allowedUsers = ConfigurationManager.AppSettings["authorizedusers"].Split(';');
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = base.AuthorizeCore(httpContext);
            return result && _allowedUsers.Contains(GetEmailClaim(httpContext.User as ClaimsPrincipal));
        }

        private string GetEmailClaim(ClaimsPrincipal principal)
        {
            string claimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
            var emailClaim = principal.Claims.SingleOrDefault(c => c.Type == claimType);
            Debug.Assert(emailClaim != null);

            return emailClaim.Value;
        }
    }
}

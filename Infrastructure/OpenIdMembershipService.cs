using Core.Interface;
using Core.Model;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.Messaging;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IlseLeijten.Models;

namespace Infrastructure
{
    public class OpenIdMembershipService : IMembershipService
    {
        private readonly OpenIdRelyingParty _relyingParty;

        public OpenIdMembershipService()
        {
            _relyingParty = new OpenIdRelyingParty();
        }

        public ActionResult RedirectToAuthenticationProvider(string openIdIdentifier)
        {
            IAuthenticationRequest openIdRequest = _relyingParty.CreateRequest(Identifier.Parse(openIdIdentifier));

            var fields = new ClaimsRequest()
            {
                Email = DemandLevel.Require,
                FullName = DemandLevel.Require,
                Nickname = DemandLevel.Require
            };
            openIdRequest.AddExtension(fields);

            return openIdRequest.RedirectingResponse.AsActionResultMvc5();
        }

        public User GetUser()
        {
            OpenIdUser user = null;
            IAuthenticationResponse openIdResponse = _relyingParty.GetResponse();

            if (openIdResponse.IsSuccessful())
            {
                user = ResponseIntoUser(openIdResponse);
            }

            return user;
        }

        private OpenIdUser ResponseIntoUser(IAuthenticationResponse response)
        {
            OpenIdUser user = null;
            var claimResponseUntrusted = response.GetUntrustedExtension<ClaimsResponse>();
            var claimResponse = response.GetExtension<ClaimsResponse>();

            if (claimResponse != null)
            {
                user = new OpenIdUser(claimResponse, response.ClaimedIdentifier);
            }
            else if (claimResponseUntrusted != null)
            {
                user = new OpenIdUser(claimResponseUntrusted, response.ClaimedIdentifier);
            }

            return user;
        }

        public HttpCookie CreateFormsAuthenticationCookie(User user)
        {
            var ticket = new FormsAuthenticationTicket(1, user.Nickname, DateTime.Now, DateTime.Now.AddDays(7), true, user.ToString());
            var encrypted = FormsAuthentication.Encrypt(ticket).ToString();
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);

            return cookie;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }

    public static class Extensions
    {
        public static bool IsSuccessful(this IAuthenticationResponse response)
        {
            return response != null && response.Status == AuthenticationStatus.Authenticated;
        }
    }
}
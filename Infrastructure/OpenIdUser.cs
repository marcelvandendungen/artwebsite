using Core.Interface;
using Core.Model;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using Infrastructure;

namespace IlseLeijten.Models
{
    public class OpenIdUser : User
    {
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public bool IsSignedByProvider { get; set; }
        public string ClaimedIdentifier { get; set; }

        public OpenIdUser(ClaimsResponse claim, string identifier)
        {
            AddClaimInfo(claim, identifier);
        }

        private void AddClaimInfo(ClaimsResponse claim, string identifier)
        {
            Email = claim.Email;
            FullName = claim.FullName;
            Nickname = claim.Nickname ?? claim.Email;
            IsSignedByProvider = claim.IsSignedByProvider;
            ClaimedIdentifier = identifier;
        }
    }
}

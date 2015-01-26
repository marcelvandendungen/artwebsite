using Core.Interface;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infrastructure
{
    public class AuthorizedUserManager : IAuthorizedUserManager
    {
        private IEnumerable<string> _authorizedUsers;

        public AuthorizedUserManager(IEnumerable<string> authorizedUsers)
        {
            _authorizedUsers = authorizedUsers;
        }

        public bool IsAllowedUser(string email)
        {
            return _authorizedUsers.Contains(email);
        }
    }
}
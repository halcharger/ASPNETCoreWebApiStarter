using System.Collections.Generic;
using System.Security.Claims;
using StarterProject.Common;

namespace StarterProject.Commands.Users
{
    public class LoginCommand : Command<Result<IList<Claim>>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
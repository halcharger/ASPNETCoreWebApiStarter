using System.Collections.Generic;
using System.Security.Claims;
using StarterProject.WebApi.Common;

namespace StarterProject.WebApi.Commands.Users
{
    public class LoginCommand : Command<Result<IList<Claim>>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
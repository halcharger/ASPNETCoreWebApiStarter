using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace StarterProject.WebApi.Common.Auth
{
    public class LoggedOnUser : ILoggedOnUserProvider
    {
        private readonly IEnumerable<Claim> claims;
        public LoggedOnUser(IHttpContextAccessor httpContextAccessor)
        {
            claims = httpContextAccessor.HttpContext.User?.Claims;
            UserId = GetStringClaimTypeValue(ClaimConstants.UserId);
            Username = GetStringClaimTypeValue(ClaimConstants.Username);
            Email = GetStringClaimTypeValue(ClaimConstants.Email);
            Roles = GetRoles();
        }
        public string UserId { get; }
        public string Username { get; }
        public string Email { get; }
        public string[] Roles { get; }

        private string GetStringClaimTypeValue(string claimType)
        {
            return claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
        }

        private string[] GetRoles()
        {
            return claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

    }
}
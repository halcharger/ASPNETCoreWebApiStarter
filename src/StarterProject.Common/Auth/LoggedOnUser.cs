using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace StarterProject.Common.Auth
{
    public class LoggedOnUser : ILoggedOnUserProvider
    {
        private readonly IEnumerable<Claim> claims;
        public LoggedOnUser(IHttpContextAccessor httpContextAccessor)
        {
            claims = httpContextAccessor.HttpContext.User?.Claims;
            UserId = GetStringClaimTypeValue(ClaimsIdentityConstants.UserId);
            Username = GetStringClaimTypeValue(ClaimsIdentityConstants.Username);
            Email = GetStringClaimTypeValue(ClaimsIdentityConstants.Email);
        }
        public string UserId { get; }
        public string Username { get; }
        public string Email { get; }

        private string GetStringClaimTypeValue(string claimType)
        {
            return claims.FirstOrDefault(c => c.Type == claimType)?.Value ?? string.Empty;
        }

    }
}
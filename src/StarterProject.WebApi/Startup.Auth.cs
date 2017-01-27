using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StarterProject.Data.Entities;
using StarterProject.WebApi.OAuth;

namespace StarterProject.WebApi
{
    //for reference see: https://stormpath.com/blog/token-authentication-asp-net-core
    public partial class Startup
    {
        // The secret key every token will be signed with.
        // Keep this safe on the server!
        private static readonly string secretKey = "mysupersecret_secretkey!123";

        private void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = "ExampleAudience",
                Issuer = "ExampleIssuer",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = "ExampleIssuer",

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = "ExampleAudience",

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            //Use only ONE of the below options:

            //1. Configuring sending JWT in Authorization header in each request
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            //2. Confgure sending JWT in cookie in each request.
            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AutomaticAuthenticate = true,
            //    AutomaticChallenge = true,
            //    AuthenticationScheme = "Cookie",
            //    CookieName = "access_token",
            //    TicketDataFormat = new CustomJwtDataFormat(
            //        SecurityAlgorithms.HmacSha256,
            //        tokenValidationParameters)
            //});
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            //get SignInManager from DI
            var signInManager = ApplicationContainer.Resolve<SignInManager<User>>();
            var userManager = ApplicationContainer.Resolve<UserManager<User>>();

            if (signInManager == null) throw new ArgumentNullException("Could not retrieve SignInManager from Autofac");
            if (userManager == null) throw new ArgumentNullException("Could not retrieve UserManager from Autofac");

            var loginResult = await signInManager.PasswordSignInAsync(username, password, false, false);

            if (loginResult.Succeeded)
            {
                var user = await userManager.FindByNameAsync(username);
                var claims = await userManager.GetClaimsAsync(user);

                return new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }
    }
}

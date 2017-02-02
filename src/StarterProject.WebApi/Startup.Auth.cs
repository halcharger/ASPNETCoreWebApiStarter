using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using StarterProject.Commands.Users;
using StarterProject.WebApi.OAuth;

namespace StarterProject.WebApi
{
    //for reference see: https://stormpath.com/blog/token-authentication-asp-net-core
    public partial class Startup
    {
        // The secret key every token will be signed with.
        // Keep this safe on the server!
        private static readonly string secretKey = "mysupersecret_secretkey!123";

        protected void ConfigureAuth(IApplicationBuilder app)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var audience = "http://localhost:65018/";
            var issuer = "PutYourIssuerHere";

            app.UseIdentity();

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = "/api/token",
                Audience = audience,
                Issuer = issuer,
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
                ValidIssuer = issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                RequireExpirationTime = true,
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            //Use only ONE of the below options:

            //1. Configuring reading JWT from Authorization header in each request
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var mediatr = ApplicationContainer.Resolve<IMediator>();

            var result = await mediatr.Send(new LoginCommand { Username = username, Password = password });

            return result.IsSuccess
                ? new ClaimsIdentity(result.Value)
                : null;
        }
    }
}

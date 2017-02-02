using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StarterProject.Common;
using StarterProject.Common.Auth;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.Users
{
    public class LoginCommandHandler : IAsyncRequestHandler<LoginCommand, Result<IList<Claim>>>
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<Result<IList<Claim>>> Handle(LoginCommand cmd)
        {
            var loginResult = await signInManager.PasswordSignInAsync(cmd.Username, cmd.Password, false, false);

            if (loginResult.Succeeded)
            {
                var user = await userManager.FindByNameAsync(cmd.Username);
                var claims = await userManager.GetClaimsAsync(user);

                claims.Add(new Claim(ClaimsIdentityConstants.UserId, user.Id));
                claims.Add(new Claim(ClaimsIdentityConstants.Username, user.UserName));
                claims.Add(new Claim(ClaimsIdentityConstants.Email, user.Email));

                return new SuccessResult<IList<Claim>>(claims);
            }

            // Credentials are invalid, or account doesn't exist
            return new FailureResult<IList<Claim>>("Invalid username or password.");
        }
    }
}
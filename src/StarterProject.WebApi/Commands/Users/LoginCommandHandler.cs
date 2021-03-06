﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NExtensions.Core;
using StarterProject.WebApi.Common;
using StarterProject.WebApi.Common.Auth;
using StarterProject.WebApi.Data.Entities;

namespace StarterProject.WebApi.Commands.Users
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
                var roles = await userManager.GetRolesAsync(user);

                claims.Add(new Claim(ClaimConstants.UserId, user.Id));
                claims.Add(new Claim(ClaimConstants.Username, user.UserName));
                claims.Add(new Claim(ClaimConstants.Email, user.Email));
                roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));

                return new SuccessResult<IList<Claim>>(claims);
            }

            // Credentials are invalid, or account doesn't exist
            return new FailureResult<IList<Claim>>("Invalid username or password.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarterProject.Commands.Users;
using StarterProject.Queries.Users;
using StarterProject.ViewModels;
using StarterProject.WebApi.Extensions;

namespace StarterProject.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator mediatr;
        private readonly IServiceProvider serviceProvider;

        public UserController(IMediator mediatr, IServiceProvider serviceProvider)
        {
            this.mediatr = mediatr;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await mediatr.Send(new UsersQuery());
        }

        [HttpPost]
        [Route("api/users/save")]
        public async Task<IActionResult> SaveUser(SaveUserCommand cmd)
        {
            var result = await mediatr.Send(cmd);

            return result.ToActionResult();
        }

        [HttpGet]
        [Authorize]
        [Route("api/users/authorized")]
        public async Task<IEnumerable<UserViewModel>> GetUsersAuthorized()
        {
            return await mediatr.Send(new UsersQuery());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarterProject.Commands.Users;
using StarterProject.Common.Auth;
using StarterProject.Queries.Users;
using StarterProject.ViewModels;
using StarterProject.WebApi.Extensions;

namespace StarterProject.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator mediatr;

        public UserController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpPost]
        [Route("api/users/register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand cmd)
        {
            var result = await mediatr.Send(cmd);
            return result.ToActionResult();
        }

        [HttpGet]
        [Authorize(Roles = RoleConstamts.Manager)]
        [Route("api/users")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await mediatr.Send(new UsersQuery());
        }

        [HttpPost]
        [Route("api/users/save")]
        public async Task<IActionResult> SaveUser([FromBody]SaveUserCommand cmd)
        {
            var result = await mediatr.Send(cmd);

            return result.ToActionResult();
        }
    }
}
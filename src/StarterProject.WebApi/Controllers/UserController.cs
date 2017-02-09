using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarterProject.WebApi.Commands.Users;
using StarterProject.WebApi.Common.Auth;
using StarterProject.WebApi.Extensions;
using StarterProject.WebApi.Queries.Users;
using StarterProject.WebApi.ViewModels.Users;

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
        [AllowAnonymous]
        [Route("api/user/register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand cmd)
        {
            var result = await mediatr.Send(cmd);
            return result.ToActionResult();
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/users")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await mediatr.Send(new UsersQuery());
        }

        [HttpGet]
        [Authorize]
        [Route("api/user")]
        public async Task<UserViewModel> GetUser(UserQuery qry)
        {
            return await mediatr.Send(qry);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        [Route("api/user/update")]
        public async Task<IActionResult> UpdateUserDetails([FromBody]UpdateUserDetailsCommand cmd)
        {
            var result = await mediatr.Send(cmd);
            return result.ToActionResult();
        }
    }
}
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StarterProject.Common;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.Users
{
    public class RegisterUserCommandHandler : IAsyncRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public RegisterUserCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand cmd)
        {
            var user = mapper.Map<User>(cmd);
            var registerResult = await userManager.CreateAsync(user, cmd.Password);

            return registerResult.Succeeded
                ? (Result<string>)new SuccessResult<string>(user.Id)
                : (Result<string>)new FailureResult<string>(registerResult.Errors.Select(e => $"{e.Code} : {e.Description}"));
        }
    }
}
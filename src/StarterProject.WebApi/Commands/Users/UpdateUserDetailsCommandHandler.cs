using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StarterProject.WebApi.Common;
using StarterProject.WebApi.Data.Entities;

namespace StarterProject.WebApi.Commands.Users
{
    public class UpdateUserDetailsCommandHandler : IAsyncRequestHandler<UpdateUserDetailsCommand, Result>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UpdateUserDetailsCommandHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Result> Handle(UpdateUserDetailsCommand cmd)
        {
            var user = await userManager.FindByIdAsync(cmd.Id);

            mapper.Map(cmd, user);

            await userManager.UpdateAsync(user);

            return new SuccessResult();
        }
    }
}
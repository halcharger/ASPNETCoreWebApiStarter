using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NExtensions.Core;
using StarterProject.Common;
using StarterProject.Data;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommandHandler : IAsyncRequestHandler<SaveUserCommand, Result<string>>
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public SaveUserCommandHandler(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<string>> Handle(SaveUserCommand cmd)
        {
            var user = new User();

            if (cmd.Id.HasValue()) user = await context.Users.FindAsync(cmd.Id);

            mapper.Map(cmd, user);

            if (cmd.Id.IsNullOrEmpty()) context.Add(user);

            await context.SaveChangesAsync();

            return new SuccessResult<string>(user.Id);
        }
    }
}
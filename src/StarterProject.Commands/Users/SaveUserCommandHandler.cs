using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StarterProject.Common;
using StarterProject.Data;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommandHandler : IAsyncRequestHandler<SaveUserCommand, Result<int>>
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public SaveUserCommandHandler(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Result<int>> Handle(SaveUserCommand cmd)
        {
            var user = new User();

            if (cmd.Id > 0) user = await context.Users.FindAsync(cmd.Id);

            mapper.Map(cmd, user);

            if (cmd.Id == 0)
            {
                context.Add(user);
            }

            await context.SaveChangesAsync();

            return new SuccessResult<int>(user.Id);
        }
    }
}
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                //This code is to facilitate the default in-memory database, it will need to be changed for actual live scenarios that use a persistent database with auto identity increment like SQL Server
                var maxId = await context.Users.MaxAsync(u => u.Id);
                user.Id = maxId + 1;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();

            return new SuccessResult<int>(user.Id);
        }
    }
}
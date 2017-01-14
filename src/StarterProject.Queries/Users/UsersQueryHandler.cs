using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarterProject.Data;
using StarterProject.ViewModels;

namespace StarterProject.Queries.Users
{
    public class UsersQueryHandler : IAsyncRequestHandler<UsersQuery, IEnumerable<UserViewModel>>
    {
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public UsersQueryHandler(IMapper mapper, AppDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(UsersQuery message)
        {
            var users = await context.Users.ToListAsync();

            var viewmodels = users.Select(mapper.Map<UserViewModel>).ToList();

            return viewmodels;
        }
    }
}
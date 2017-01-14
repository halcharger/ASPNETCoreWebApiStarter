using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using StarterProject.Data.Entities;
using StarterProject.ViewModels;

namespace StarterProject.Queries.Users
{
    public class UsersQueryHandler : IAsyncRequestHandler<UsersQuery, IEnumerable<UserViewModel>>
    {
        private readonly IMapper mapper;

        public UsersQueryHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<UserViewModel>> Handle(UsersQuery message)
        {
            var users = new[]
{
                new User {Id = 1, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
                new User {Id = 2, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
                new User {Id = 3, Email = $"{Guid.NewGuid()}@gmail.com", FullName = Guid.NewGuid().ToString()},
            };

            var viewmodels = users.Select(mapper.Map<UserViewModel>).ToList();

            return viewmodels;
        }
    }
}
using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NExtensions.Core;
using StarterProject.Data.Entities;
using StarterProject.ViewModels;

namespace StarterProject.Queries.Users
{
    public class UserQueryHandler : IAsyncRequestHandler<UserQuery, UserViewModel>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UserQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserViewModel> Handle(UserQuery qry)
        {
            User user = null;

            if (qry.Id.HasValue())
                user = await userManager.FindByIdAsync(qry.Id);
            else if (qry.Username.HasValue())
                user = await userManager.FindByNameAsync(qry.Username);
            else
                throw new ArgumentException("Neither Id nor Username were provided for the UserQuery.");

            return mapper.Map<UserViewModel>(user);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StarterProject.Data.Entities;
using StarterProject.ViewModels;

namespace StarterProject.WebApi.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("api/users")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
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
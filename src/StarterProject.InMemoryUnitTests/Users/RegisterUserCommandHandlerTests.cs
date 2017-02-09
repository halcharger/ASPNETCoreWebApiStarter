using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NExtensions.Core;
using NUnit.Framework;
using StarterProject.WebApi.Commands.Users;
using StarterProject.WebApi.Data.Entities;

namespace StarterProject.InMemoryUnitTests.Users
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests : InMemoryHandlerTests
    {
        protected RegisterUserCommandHandler GetHandler()
        {
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            var manager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            return new RegisterUserCommandHandler(manager, mapper, roleManager);
        }

        [Test]
        public async Task RegisterUserAddsNewUserToDbContext()
        {
            var handler = GetHandler();
            var cmd = new RegisterUserCommand
            {
                FullName = Guid.NewGuid().ToString(), 
                Email = $"{Guid.NewGuid()}@gmail.com", 
                UserName = Guid.NewGuid().ToString(), 
                Password = Guid.NewGuid() + "U"//to satify the requirement for passwords to have at least 1 uppercase letter
            };

            var result = await handler.Handle(cmd);

            result.IsSuccess.Should().BeTrue(result.Failures.JoinWithComma());
            await AssertUserExistsInContext(result.Value, cmd.FullName, cmd.Email, cmd.UserName);
        }

    }
}
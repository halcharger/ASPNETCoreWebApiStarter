using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StarterProject.Commands.Users;
using StarterProject.Data;
using StarterProject.Data.Entities;
using StarterProject.WebApi;

namespace StarterProject.InMemoryUnitTests.Users
{
    [TestFixture]
    public class SaveUserCommandHandlerTests
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddScoped<AppDbContext>();

            services.AddAutoMapperWithProfiles();

            serviceProvider = services.BuildServiceProvider();
        }

        protected SaveUserCommandHandler GetHandler()
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var mapper = serviceProvider.GetRequiredService<IMapper>();
            return new SaveUserCommandHandler(context, mapper);
        }

        [Test]
        public async Task SaveUserAddsNewUserToContext()
        {
            var handler = GetHandler();
            var cmd = new SaveUserCommand
            {
                Id = null,
                FullName = Guid.NewGuid().ToString(), 
                Email = $"{Guid.NewGuid()}@gmail.com", 
                UserName = Guid.NewGuid().ToString()
            };

            var result = await handler.Handle(cmd);

            result.IsSuccess.Should().BeTrue();

            await AssertUserExistsInContext(result.Value, cmd.FullName, cmd.Email, cmd.UserName);
        }

        [Test]
        public async Task SaveUserUpdatesExistingUserInContext()
        {
            //setup | add existing user to db context
            var existingUserId = Guid.NewGuid().ToString();
            await AddUserToContext(existingUserId, "allen firth", "allen.firth@gmail.com", Guid.NewGuid().ToString());

            var handler = GetHandler();
            var cmd = new SaveUserCommand
            {
                Id = existingUserId,
                FullName = "Robbie", 
                Email = "robbie@gmail.com", 
                UserName = Guid.NewGuid().ToString()
            };

            var result = await handler.Handle(cmd);

            result.IsSuccess.Should().BeTrue();

            await AssertUserExistsInContext(cmd.Id, cmd.FullName, cmd.Email, cmd.UserName);
        }

        private async Task AssertUserExistsInContext(string id, string fullName, string email, string username)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id && u.FullName == fullName && u.Email == email && u.UserName == username);

            if (user == null) Assert.Fail($"Could not find User in DbContext with Id: {id}, FullName: {fullName}, Email: {email}, Username: {username}");
        }

        private async Task AddUserToContext(string id, string fullName, string email, string username)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Users.Add(new User {Id = id, FullName = fullName, Email = email, UserName = username});
            await context.SaveChangesAsync();
        }
    }
}
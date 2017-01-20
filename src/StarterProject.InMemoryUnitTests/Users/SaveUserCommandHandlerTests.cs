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
                Id = 0,
                FullName = Guid.NewGuid().ToString(), 
                Email = $"{Guid.NewGuid()}@gmail.com"
            };

            var result = await handler.Handle(cmd);

            result.IsSuccess.Should().BeTrue();

            await AssertUserExistsInContext(1, cmd.FullName, cmd.Email);
        }

        [Test]
        public async Task SaveUserUpdatesExistingUserInContext()
        {
            //setup | add existing user to db context
            await AddUserToContext(1, "allen firth", "allen.firth@gmail.com");

            var handler = GetHandler();
            var cmd = new SaveUserCommand
            {
                Id = 1,
                FullName = "Robbie", 
                Email = "robbie@gmail.com"
            };

            var result = await handler.Handle(cmd);

            result.IsSuccess.Should().BeTrue();

            await AssertUserExistsInContext(cmd.Id, cmd.FullName, cmd.Email);
        }

        private async Task AssertUserExistsInContext(int id, string fullName, string email)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id && u.FullName == fullName && u.Email == email);

            if (user == null) Assert.Fail($"Could not find User in DbContext with Id: {id}, FullName: {fullName}, Email: {email}");
        }

        private async Task AddUserToContext(int id, string fullName, string email)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Users.Add(new User {Id = id, FullName = fullName, Email = email});
            await context.SaveChangesAsync();
        }
    }
}
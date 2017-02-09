using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using StarterProject.WebApi;
using StarterProject.WebApi.Data;

namespace StarterProject.InMemoryUnitTests
{
    [TestFixture]
    public abstract class InMemoryHandlerTests
    {
        protected IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddScoped<AppDbContext>();

            services.AddAutoMapperWithProfiles();
            services.AddAspNetIdentity();

            serviceProvider = services.BuildServiceProvider();
        }

        protected async Task AssertUserExistsInContext(string id, string fullName, string email, string username)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var user = await context.Users.SingleOrDefaultAsync(u => u.Id == id && u.FullName == fullName && u.Email == email && u.UserName == username);

            if (user == null) Assert.Fail($"Could not find User in DbContext with Id: {id}, FullName: {fullName}, Email: {email}, Username: {username}");
        }

    }
}
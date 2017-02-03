using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using StarterProject.Commands.Users;

namespace StarterProject.IntegrationTests.Users
{
    [TestFixture]
    public class UsersIntegrationTests : IntegrationTest
    {
        [Test]
        public async Task CanRegisterNewUserAndLogin()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid()}@gmail.com",
                UserName = Guid.NewGuid().ToString(),
                Password = $"U{Guid.NewGuid()}"
            };

            var registerResult = await RegisterUser(cmd);
            Console.WriteLine($"new registered user Id: {registerResult}");

            var loginResult = await Server.Login(cmd.UserName, cmd.Password);
            Console.WriteLine($"login result: {loginResult}");

            var userDetailsResult = await Server.Get("api/utility/userdetails");
            Console.WriteLine($"user details result: {userDetailsResult}");

            dynamic userDetails = JsonConvert.DeserializeObject(userDetailsResult);

            string userId = userDetails.userId;
            userId.Should().NotBeNullOrEmpty();

            string email = userDetails.email;
            email.Should().Be(cmd.Email);

            string username = userDetails.username;
            username.Should().Be(cmd.UserName);
        }

        public async Task<string> RegisterUser(RegisterUserCommand cmd)
        {
            return await Server.Post("api/users/register", cmd);
        }
    }
}
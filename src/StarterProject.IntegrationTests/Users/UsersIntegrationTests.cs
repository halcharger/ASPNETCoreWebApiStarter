using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json.Linq;
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

            var json = JObject.Parse(userDetailsResult);

            json["userId"].ToString().Should().NotBeNullOrEmpty();
            json["email"].ToString().Should().Be(cmd.Email);
            json["username"].ToString().Should().Be(cmd.UserName);

        }

        public async Task<string> RegisterUser(RegisterUserCommand cmd)
        {
            return await Server.Post("api/users/register", cmd);
        }
    }
}
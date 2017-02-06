using System;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using StarterProject.Commands.Users;
using StarterProject.Common.Auth;
using StarterProject.ViewModels;

namespace StarterProject.IntegrationTests.Users
{
    [TestFixture]
    public class UsersIntegrationTests : IntegrationTest
    {
        [Test]
        public async Task CanRegisterNewUserAndLogin()
        {
            var cmd = await RegisterAndLoginUser();

            var userDetailsResult = await Server.Get("api/utility/userdetails");
            Console.WriteLine($"user details result: {userDetailsResult}");

            var json = JObject.Parse(userDetailsResult);

            json["userId"].ToString().Should().NotBeNullOrEmpty();
            json["email"].ToString().Should().Be(cmd.Email);
            json["username"].ToString().Should().Be(cmd.UserName);
        }

        [Test]
        public async Task CanRegisterThenUpdateUser()
        {
            var registerCmd = await RegisterAndLoginUser();

            var registeredUser = await GetUser(registerCmd.UserName);

            registeredUser.Email.Should().Be(registerCmd.Email);
            registeredUser.FullName.Should().Be(registerCmd.FullName);

            var updateCmd = new UpdateUserDetailsCommand
            {
                Id = registeredUser.Id,
                FullName = Guid.NewGuid().ToString(), 
                Email = $"{Guid.NewGuid()}@gmail.com"
            };

            await Server.Post("api/user/update", updateCmd);

            var updatedUser = await GetUser(registeredUser.UserName);

            updatedUser.Id.Should().Be(registeredUser.Id);
            updatedUser.Email.Should().Be(updateCmd.Email);
            updatedUser.FullName.Should().Be(updateCmd.FullName);
        }

        protected async Task<RegisterUserCommand> RegisterAndLoginUser()
        {
            var cmd = new RegisterUserCommand
            {
                FullName = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid()}@gmail.com",
                UserName = Guid.NewGuid().ToString(),
                Password = $"U{Guid.NewGuid()}", 
                Roles = new[] {RoleConstants.Admin}
            };

            var registerResult = await RegisterUser(cmd);
            Console.WriteLine($"new registered user Id: {registerResult}");

            var loginResult = await Server.Login(cmd.UserName, cmd.Password);
            Console.WriteLine($"login result: {loginResult}");

            return cmd;
        }

        protected async Task<UserViewModel> GetUser(string username)
        {
            var registeredUser = await Server.Get($"api/user?username={username}");
            return JsonConvert.DeserializeObject<UserViewModel>(registeredUser);
        }

        protected async Task<string> RegisterUser(RegisterUserCommand cmd)
        {
            return await Server.Post("api/user/register", cmd);
        }
    }
}
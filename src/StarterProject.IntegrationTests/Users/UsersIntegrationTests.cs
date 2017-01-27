using System;
using System.Threading.Tasks;
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
            var cmd = new RegisterUserCommand{FullName = Guid.NewGuid().ToString(), Email = $"{Guid.NewGuid()}@gmail.com", UserName = Guid.NewGuid().ToString(), Password = $"U{Guid.NewGuid()}"};

            var registerResult = await RegisterUser(cmd);
            Console.WriteLine($"new registered user Id: {registerResult}");

            var loginResult = await Server.Login(cmd.UserName, cmd.Password);
            Console.WriteLine($"login result: {loginResult}");
        }

        public async Task<string> RegisterUser(RegisterUserCommand cmd)
        {
            return await Server.Post("api/users/register", cmd);
        }

        //public async Task<IEnumerable<UserViewModel>> GetUsers()
        //{
        //    var response = await Client.GetAsync("api/users");

        //    response.EnsureSuccessStatusCode();

        //    var users = await response.Content.ReadAsStringAsync();

        //    return JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(users);
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using StarterProject.Commands.Users;
using StarterProject.ViewModels;

namespace StarterProject.IntegrationTests.Users
{
    [TestFixture]
    public class UsersIntegrationTests : IntegrationTest
    {
        [Test]
        public async Task CanAddNewUser()
        {
            var cmd = new SaveUserCommand{FullName = Guid.NewGuid().ToString(), Email = $"{Guid.NewGuid()}@gmail.com"};

            var newUserId = await AddUser(cmd);
            newUserId.Should().Be("1");

            var users = await GetUsers();

            users.Count().Should().Be(1);
            users.First().ShouldEqual(cmd, ignore:"Id");
        }

        public async Task<string> AddUser(SaveUserCommand cmd)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("api/users/save", content);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var response = await Client.GetAsync("api/users");

            response.EnsureSuccessStatusCode();

            var users = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<UserViewModel>>(users);
        }
    }
}
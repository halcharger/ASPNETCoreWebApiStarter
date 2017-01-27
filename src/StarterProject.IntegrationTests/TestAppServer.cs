using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace StarterProject.IntegrationTests
{
    public class TestAppServer : IDisposable
    {
        protected TestServer Server;
        protected HttpClient Client;

        public TestAppServer()
        {
            var builder = new WebHostBuilder().UseStartup<IntegrationTestStartup>();

            Server = new TestServer(builder);
            Client = Server.CreateClient();

        }

        public void Dispose()
        {
            Client?.Dispose();
            Server?.Dispose();
        }

        public async Task<string> Post(string route, object cmd)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(route, content);

            var result = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue(result);

            return result;
        }

        public async Task<string> Login(string username, string password)
        {
            var content = new FormUrlEncodedContent(new []{new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password)});

            var response = await Client.PostAsync("api/token", content);

            var result = await response.Content.ReadAsStringAsync();

            response.IsSuccessStatusCode.Should().BeTrue();

            return result;
        }
    }
}
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

        protected string jwt = string.Empty;

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
            var response = await Server.CreateRequestWithAuthHeader(jwt, route, cmd).PostAsync();

            return await CheckAndReturnResponseContent(response);
        }

        public async Task<string> Get(string route)
        {
            var response = await Server.CreateRequestWithAuthHeader(jwt, route).GetAsync();

            return await CheckAndReturnResponseContent(response);
        }

        public async Task<string> Login(string username, string password)
        {
            var content = new FormUrlEncodedContent(new []{new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password)});

            var response = await Client.PostAsync("api/token", content);

            var responseContent =  await CheckAndReturnResponseContent(response);

            dynamic data = JsonConvert.DeserializeObject(responseContent);
            jwt = data.access_token;

            return responseContent;
        }

        private async Task<string> CheckAndReturnResponseContent(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return result;

            response.IsSuccessStatusCode.Should().BeTrue(result);

            return result;
        }
    }
}
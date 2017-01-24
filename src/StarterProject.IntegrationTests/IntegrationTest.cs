using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace StarterProject.IntegrationTests
{
    [TestFixture]
    public class IntegrationTest
    {
        protected TestServer Server;
        protected HttpClient Client;

        [OneTimeSetUp]
        public virtual void OnTimeSetUp()
        {
            var builder = new WebHostBuilder().UseStartup<IntegrationTestStartup>();

            Server = new TestServer(builder);
            Client = Server.CreateClient();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            Client?.Dispose();
            Server?.Dispose();
        }
    }
}
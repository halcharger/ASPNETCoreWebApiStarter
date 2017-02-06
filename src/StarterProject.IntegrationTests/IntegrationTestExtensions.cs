using System.Linq;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace StarterProject.IntegrationTests
{
    public static class IntegrationTestExtensions
    {
        public static void ShouldEqual(this object one, object two, params string[] ignore)
        {
            var compare = new CompareLogic(new ComparisonConfig
            {
                IgnoreObjectTypes = true,
                MembersToIgnore = ignore.ToList(),
                MaxDifferences = int.MaxValue,//show all differences
            });
            var result = compare.Compare(one, two);
            
            result.AreEqual.Should().BeTrue(result.DifferencesString);
        }

        public static void AddAuthHeader(this RequestBuilder request, string jwt)
        {
            request.AddHeader("Authorization", $"Bearer {jwt}");
        }

        public static RequestBuilder CreateRequestWithAuthHeader(this TestServer server, string jwt, string route, object cmd = null)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cmd), Encoding.UTF8, "application/json");

            var request = server.CreateRequest(route);
            request.AddAuthHeader(jwt);

            if (cmd != null) request = request.And(msg => msg.Content = content);

            return request;
        }
    }
}
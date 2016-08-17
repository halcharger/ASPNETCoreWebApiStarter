using System.Reflection;
using System.Threading.Tasks;
using ASPNETCoreWebApiStarter.Common;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebApiStarter.Controllers
{
    public class VersionController
    {
        private readonly AppSettings settings;

        public VersionController(AppSettings settings)
        {
            this.settings = settings;
        }

        [HttpGet]
        [Route("api/version")]
        public async Task<string> GetVersion()
        {
            return $"{settings.Environment}: {Assembly.GetEntryAssembly().GetName().Version}";
        }
    }
}
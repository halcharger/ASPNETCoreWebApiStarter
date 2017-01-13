using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarterProject.Common;

namespace StarterProject.WebApi.Controllers
{
    public class VersionController : Controller
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
            return $"{settings.Environment} 1.0.0.0";
        }
    }
}
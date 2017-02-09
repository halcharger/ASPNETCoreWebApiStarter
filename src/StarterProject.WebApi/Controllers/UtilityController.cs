using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StarterProject.WebApi.Common;
using StarterProject.WebApi.Common.Auth;

namespace StarterProject.WebApi.Controllers
{
    public class UtilityController : Controller
    {
        private readonly AppSettings settings;
        private readonly ILoggedOnUserProvider user;

        public UtilityController(AppSettings settings, ILoggedOnUserProvider user)
        {
            this.settings = settings;
            this.user = user;
        }

        [HttpGet]
        [Route("api/version")]
        public string GetVersion()
        {
            return $"{settings.Environment} 1.0.0.0";
        }

        [HttpGet]
        [Authorize]
        [Route("api/utility/userdetails")]
        public IActionResult GetLoggedOnUserDetails()
        {
            return new JsonResult(user, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

    }
}
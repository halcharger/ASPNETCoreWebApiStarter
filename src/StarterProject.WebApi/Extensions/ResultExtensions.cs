using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StarterProject.Common;

namespace StarterProject.WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsFailure)
            {
                if (result.HasException)
                {
                    //TODO what error reporting tool will we use to log errors?
                    //ErrorSignal.FromCurrentContext().Raise(result.Exception);
                }
                return new BadRequestObjectResult(result.HtmlFormattedFailures);
            }

            return new OkResult();
        }

        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsFailure)
            {
                if (result.HasException)
                {
                    //TODO what error reporting tool will we use to log errors?
                    //ErrorSignal.FromCurrentContext().Raise(result.Exception);
                }
                return new BadRequestObjectResult(result.HtmlFormattedFailures);
            }

            return new JsonResult(result.Value, new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }
    }
}
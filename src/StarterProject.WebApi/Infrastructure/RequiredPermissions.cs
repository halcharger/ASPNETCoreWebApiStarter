using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StarterProject.WebApi.Infrastructure
{
    public class RequiredPermissions : AuthorizationFilterContext
    {
        public RequiredPermissions(ActionContext actionContext, IList<IFilterMetadata> filters) : base(actionContext, filters)
        {
        }
    }
}
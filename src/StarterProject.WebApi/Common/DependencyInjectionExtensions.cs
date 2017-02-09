using Microsoft.Extensions.DependencyInjection;
using StarterProject.WebApi.Common.Auth;

namespace StarterProject.WebApi.Common
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggedOnUserProvider, LoggedOnUser>();
        }
    }
}
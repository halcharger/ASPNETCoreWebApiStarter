using Microsoft.Extensions.DependencyInjection;
using StarterProject.Common.Auth;

namespace StarterProject.Common.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ILoggedOnUserProvider, LoggedOnUser>();
        }
    }
}
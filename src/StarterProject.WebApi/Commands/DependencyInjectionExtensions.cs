using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.WebApi.Commands.Mediatr;

namespace StarterProject.WebApi.Commands
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureDependecyInjectionForCommandPipelineBehaviours(this IServiceCollection services)
        {
            //Pipelines are executed in the order in which they are registered:

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FluentValidationPipelineBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandAuditPipelineBehaviour<,>));
        }
    }
}
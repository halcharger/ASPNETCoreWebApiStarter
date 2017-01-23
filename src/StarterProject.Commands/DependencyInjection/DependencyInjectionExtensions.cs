using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Commands.Mediatr;

namespace StarterProject.Commands.DependencyInjection
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
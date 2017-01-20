using System.Linq;
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Commands.Users;

namespace StarterProject.Commands.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureDependecyInjectionForCommandValidators(this IServiceCollection services)
        {
            var openGenericType = typeof(IValidator<>);
            var types = typeof(SaveUserCommand).GetTypeInfo().Assembly.GetTypes();


            var query = from type in types
						let interfaces = type.GetTypeInfo().ImplementedInterfaces
						let genericInterfaces = interfaces.Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == openGenericType)
						let matchingInterface = genericInterfaces.FirstOrDefault()
						where matchingInterface != null
						select new { InterfaceType = matchingInterface, ValidatorType = type};

            var findValidatorsInAssembly =  query.ToList();
            foreach (var item in findValidatorsInAssembly.Where(i => !i.ValidatorType.GetTypeInfo().IsAbstract))
            {
                services.AddScoped(item.InterfaceType, item.ValidatorType);
            }
        }
    }
}
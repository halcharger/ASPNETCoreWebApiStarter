using Microsoft.Extensions.DependencyInjection;

namespace StarterProject.Data.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static void ConfigureDependecyInjectionForData(this IServiceCollection services)
        {
            //This is where we explicitly configure the Data assembly dependency injection

            //Because we usig InMemory databasse we need the DbContext to be a singleton to keep the data in memory across request
            services.AddSingleton<AppDbContext>();

            //For persistent sotre like SQL Server, uncomment the below
            //services.AddScoped<AppDbContext>();
        }
    }
}
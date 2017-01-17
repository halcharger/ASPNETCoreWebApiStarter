using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StarterProject.Common;
using StarterProject.Data;
using StarterProject.Data.DependencyInjection;
using StarterProject.Queries.MappingProfiles;
using StarterProject.Queries.Users;

namespace StarterProject.WebApi
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddAppSettings(Configuration);
            services.AddAutoMapperWithProfiles();
            services.AddMediatrWithHandlers();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());

            services.ConfigureDependecyInjectionForData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var context = app.ApplicationServices.GetService<AppDbContext>();
            context.SeedData();//add test data to in memory database for testing, remove for actual projects

            ConfigureAuth(app);

            app.UseMvc();
        }
    }

    public static class ServicesExtensions
    {
        public static void AddAppSettings(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            var appSettings = new AppSettings();
            new ConfigureFromConfigurationOptions<AppSettings>(appSettingsSection).Configure(appSettings);
            services.Add(new ServiceDescriptor(typeof(AppSettings), appSettings));
        }

        public static void AddAutoMapperWithProfiles(this IServiceCollection services)
        {
            var assembliesContainingMappingProfiles = new[]
            {
                typeof(UserProfile)
            };
            services.AddAutoMapper(assembliesContainingMappingProfiles);
        }

        public static void AddMediatrWithHandlers(this IServiceCollection services)
        {
            var assembliesContainingMediatrHandlers = new []
            {
                typeof(UsersQueryHandler)
            };
            services.AddMediatR(assembliesContainingMediatrHandlers);
        }
    }
}

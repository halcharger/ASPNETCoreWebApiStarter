using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using StarterProject.Commands.DependencyInjection;
using StarterProject.Commands.MappingProfiles;
using StarterProject.Commands.Users;
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

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                    .AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SaveUserCommandValidator>());

            services.AddAppSettings(Configuration);
            services.AddAutoMapperWithProfiles();
            services.AddMediatrWithHandlers();
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());
            
            services.ConfigureDependecyInjectionForData();
            services.ConfigureDependecyInjectionForCommandPipelineBehaviours();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureAuth(app);

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
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
                typeof(UserProfile), 
                typeof(UserMappingProfile)
            };
            services.AddAutoMapper(assembliesContainingMappingProfiles);
        }

        public static void AddMediatrWithHandlers(this IServiceCollection services)
        {
            var assembliesContainingMediatrHandlers = new []
            {
                typeof(UsersQueryHandler),
                typeof(SaveUserCommandHandler)
            };
            services.AddMediatR(assembliesContainingMediatrHandlers);
        }
    }
}

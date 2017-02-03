using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
using StarterProject.Common.Auth;
using StarterProject.Common.DependencyInjection;
using StarterProject.Data;
using StarterProject.Data.DependencyInjection;
using StarterProject.Data.Entities;
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
            //The order of these service registrations is critical, do not randomaly change the order without knowing what you're doing.
            services.AddOptions();

            services.AddAspNetIdentity();
            //services.ConfigureAuthorizationPolicies();

            services.AddMvc()
                    .AddJsonOptions(opts => opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SaveUserCommandValidator>());

            services.AddAppSettings(Configuration);
            services.AddAutoMapperWithProfiles();
            services.AddMediatrWithHandlers();

            ConfigureDatabase(services);

            services.ConfigureCommonServices();
            services.ConfigureDataServices();
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
                //need to get relevant nuget packages to apply the below
                //app.UseDatabaseErrorPage();
                //app.UseBrowserLink();
            }

            ConfigureAuth(app);
            ConfigureSeedData();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseMvc();
        }

        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            //This will become a persistent database like SQL server in a real project
            //This method will also be overriden for the Integration Tests which will explicitly want to use an In-memory database.
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());
        }

        protected virtual void ConfigureSeedData()
        {
            var userManager = ApplicationContainer.Resolve<UserManager<User>>();
            var roleManager = ApplicationContainer.Resolve<RoleManager<IdentityRole>>();

            //create default roles
            roleManager.CreateAsync(new IdentityRole(RoleConstamts.Admin)).Wait();
            roleManager.CreateAsync(new IdentityRole(RoleConstamts.Manager)).Wait();

            //create default user
            var user = new User
            {
                UserName = "test",
                Email = "test@gmail.com",
                FullName = "Test User"
            };

            userManager.CreateAsync(user, "Testing@123").Wait();

            //assign roles to user
            userManager.AddToRoleAsync(user, RoleConstamts.Admin).Wait();
            userManager.AddToRoleAsync(user, RoleConstamts.Manager).Wait();
        }
    }

    public static class ServicesExtensions
    {
        public static void ConfigureAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(AuthPolicyConstants.IsAdmin, policy => policy.RequireClaim(ClaimConstants.IsAdmin));
            });
        }
        public static void AddAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
        }
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
            var assembliesContainingMediatrHandlers = new[]
            {
                typeof(UsersQueryHandler),
                typeof(SaveUserCommandHandler)
            };
            services.AddMediatR(assembliesContainingMediatrHandlers);
        }
    }
}

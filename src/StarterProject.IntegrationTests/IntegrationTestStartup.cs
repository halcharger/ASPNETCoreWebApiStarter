using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarterProject.Data;
using StarterProject.WebApi;

namespace StarterProject.IntegrationTests
{
    public class IntegrationTestStartup : Startup
    {
        public IntegrationTestStartup(IHostingEnvironment env) : base(env) { }

        protected override void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase());
        }
    }
}
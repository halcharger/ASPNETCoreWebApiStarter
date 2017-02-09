using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StarterProject.WebApi.Data.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
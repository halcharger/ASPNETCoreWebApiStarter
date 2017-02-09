using StarterProject.WebApi.ViewModels.Users;

namespace StarterProject.WebApi.Queries.Users
{
    public class UserQuery : Query<UserViewModel>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
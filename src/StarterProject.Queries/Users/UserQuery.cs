using StarterProject.ViewModels;

namespace StarterProject.Queries.Users
{
    public class UserQuery : Query<UserViewModel>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}
using StarterProject.Common;

namespace StarterProject.Commands.Users
{
    public class RegisterUserCommand : Command<Result<string>>
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string[] Roles { get; set; }
    }
}
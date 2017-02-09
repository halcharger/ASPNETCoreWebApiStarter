using StarterProject.WebApi.Common;

namespace StarterProject.WebApi.Commands.Users
{
    public class UpdateUserDetailsCommand : Command<Result>
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        //Depending on whether you're actualing using email as the username, you may or may not want to allow a user to change their email address
        public string Email { get; set; }
    }
}
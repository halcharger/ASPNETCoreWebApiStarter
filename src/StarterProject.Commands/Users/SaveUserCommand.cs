using MediatR;
using StarterProject.Common;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommand : Command<Result<string>>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
using MediatR;
using StarterProject.Common;

namespace StarterProject.Commands.Users
{
    public class SaveUserCommand : Command, IRequest<Result<int>>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }

    }
}
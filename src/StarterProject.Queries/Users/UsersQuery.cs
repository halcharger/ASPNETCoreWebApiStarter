using System.Collections.Generic;
using MediatR;
using StarterProject.ViewModels;

namespace StarterProject.Queries.Users
{
    public class UsersQuery : IRequest<IEnumerable<UserViewModel>>
    {
        
    }
}
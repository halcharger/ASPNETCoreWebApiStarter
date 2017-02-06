using AutoMapper;
using StarterProject.Commands.Users;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<RegisterUserCommand, User>();
            CreateMap<UpdateUserDetailsCommand, User>();
        }
    }
}
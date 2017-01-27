using AutoMapper;
using StarterProject.Commands.Users;
using StarterProject.Data.Entities;

namespace StarterProject.Commands.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<SaveUserCommand, User>();
            CreateMap<RegisterUserCommand, User>();
        }
    }
}
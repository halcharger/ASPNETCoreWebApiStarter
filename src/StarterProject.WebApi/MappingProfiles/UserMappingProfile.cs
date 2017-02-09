using AutoMapper;
using StarterProject.WebApi.Commands.Users;
using StarterProject.WebApi.Data.Entities;

namespace StarterProject.WebApi.MappingProfiles
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
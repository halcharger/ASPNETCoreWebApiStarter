using AutoMapper;
using StarterProject.WebApi.Data.Entities;
using StarterProject.WebApi.ViewModels.Users;

namespace StarterProject.WebApi.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
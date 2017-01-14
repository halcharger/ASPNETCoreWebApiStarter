using AutoMapper;
using StarterProject.Data.Entities;
using StarterProject.ViewModels;

namespace StarterProject.Queries.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
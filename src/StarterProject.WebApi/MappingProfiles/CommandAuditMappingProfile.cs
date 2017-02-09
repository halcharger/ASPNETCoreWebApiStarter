using AutoMapper;
using StarterProject.WebApi.Data.Entities;
using StarterProject.WebApi.ViewModels.Audits;

namespace StarterProject.WebApi.MappingProfiles
{
    public class CommandAuditMappingProfile : Profile
    {
        public CommandAuditMappingProfile()
        {
            CreateMap<CommandAudit, CommandAuditViewModel>();
        }
    }
}
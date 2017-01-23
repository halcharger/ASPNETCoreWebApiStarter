using AutoMapper;
using StarterProject.Data.Entities;
using StarterProject.ViewModels.Audits;

namespace StarterProject.Queries.MappingProfiles
{
    public class CommandAuditMappingProfile : Profile
    {
        public CommandAuditMappingProfile()
        {
            CreateMap<CommandAudit, CommandAuditViewModel>();
        }
    }
}
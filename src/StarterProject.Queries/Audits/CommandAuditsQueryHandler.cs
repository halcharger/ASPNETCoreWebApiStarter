using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StarterProject.Data;
using StarterProject.ViewModels.Audits;

namespace StarterProject.Queries.Audits
{
    public class CommandAuditsQueryHandler : IAsyncRequestHandler<CommandAuditsQuery, IEnumerable<CommandAuditViewModel>>
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public CommandAuditsQueryHandler(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CommandAuditViewModel>> Handle(CommandAuditsQuery qry)
        {
            var queryDate = qry.QueryDate;
            var results = await context.CommandAudits
                                       .Where(ca => ca.UtcTimeStamp.Year == queryDate.Year && ca.UtcTimeStamp.Month == queryDate.Month && ca.UtcTimeStamp.Day == queryDate.Day)
                                       .ToListAsync();

            return results.Select(mapper.Map<CommandAuditViewModel>).ToList();
        }
    }
}
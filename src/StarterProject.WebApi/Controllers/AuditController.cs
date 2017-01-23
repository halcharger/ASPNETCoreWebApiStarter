using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StarterProject.Queries.Audits;
using StarterProject.ViewModels.Audits;

namespace StarterProject.WebApi.Controllers
{
    public class AuditController : Controller
    {
        private readonly IMediator mediatr;

        public AuditController(IMediator mediatr)
        {
            this.mediatr = mediatr;
        }

        [HttpGet]
        [Route("api/audits/command/{year:int}/{month:int}/{day:int}")]
        public async Task<IEnumerable<CommandAuditViewModel>> GetCommandAudits(CommandAuditsQuery qry)
        {
            return await mediatr.Send(qry);
        }
    }
}
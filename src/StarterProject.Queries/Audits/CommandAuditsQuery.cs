using System;
using System.Collections.Generic;
using StarterProject.ViewModels.Audits;

namespace StarterProject.Queries.Audits
{
    public class CommandAuditsQuery : Query<IEnumerable<CommandAuditViewModel>>
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public DateTime QueryDate => new DateTime(Year, Month, Day);
    }
}
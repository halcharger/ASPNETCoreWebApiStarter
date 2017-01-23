using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StarterProject.Common
{
    //Base class used to commands and queries
    public abstract class Message
    {
        public int LoggedOnUserId { get; set; }

        [JsonIgnore]
        public Guid MessageId { get; set; } = Guid.NewGuid();

        [JsonIgnore]
        public DateTime UtcTimeStamp { get; set; } = DateTime.UtcNow;

        public List<string> MessageExecutionLogs { get; set; } = new List<string>();

        [JsonIgnore]
        public bool AuditThisMessage { get; private set; } = true;

        public void DontAuditThisMessage()
        {
            AuditThisMessage = false;
        }

    }
}
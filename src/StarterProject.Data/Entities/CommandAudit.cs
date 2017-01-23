using System;

namespace StarterProject.Data.Entities
{
    public class CommandAudit
    {
        public long Id { get; set; }
        public int LoggedOnUserId { get; set; }
        public virtual User LoggedOnUser { get; set; }
        public DateTime UtcTimeStamp { get; set; } = DateTime.UtcNow;
        public Guid MessageId { get; set; } = Guid.NewGuid();
        public bool IsSuccess { get; set; }
        public string ExceptionMessage { get; set; }
        public string CommandType { get; set; }
        public string CommandData { get; set; }
        public int Milliseconds { get; set; }

    }
}
using System;
using Sitecore.DevEx.Client.Tasks;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.TimeEntries
{
    public class StartTimeEntryArgs : ProjectReferenceArgs
    {
        public string Task { get; set; }

        public DateTimeOffset? Start { get; set; }
        
        public DateTimeOffset? End { get; set; }

        public override void Validate()
        {
            Require(nameof(Task));
        }
    }
}

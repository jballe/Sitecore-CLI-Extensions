using Sitecore.DevEx.Client.Tasks;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.ConnectionSetup
{
    public class InitializeClockifyArgs : TaskOptionsBase
    {
        public string ApiKey { get; set; }

        public string Workspace { get; set; }

        public string Project { get; set; }

        public override void Validate()
        {
            Require(nameof(Workspace));
        }
    }
}

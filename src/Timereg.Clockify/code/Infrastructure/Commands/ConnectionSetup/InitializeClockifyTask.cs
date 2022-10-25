using System.Diagnostics;
using System.Threading.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Services;
using SitecoreCliExtensions.Timereg.Clockify.Models;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.ConnectionSetup
{
    public class InitializeClockifyTask : TaskBase<InitializeClockifyArgs>
    {
        private readonly IUserConfigWriter _writer;
        private readonly UserConfig _config;
        public const string Name = "init";
        public const string Description = "Initialize Clockify connection";

        public InitializeClockifyTask(IUserConfigWriter writer, UserConfig config)
        {
            _writer = writer;
            _config = config;
        }

        public override async Task<int> Execute(InitializeClockifyArgs args)
        {
            args.Validate();

#if DEBUG
            Debugger.Launch();
#endif

            _config.Workspace = args.Workspace;
            _config.Project = args.Project;
            _config.ApiKey = args.ApiKey;

            await _writer.Set(_config);
            return 0;
        }
    }
}

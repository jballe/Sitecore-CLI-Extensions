using System.CommandLine;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;

namespace SitecoreCliExtensions.Workflows
{
    public class WorkflowCommand : Command, ISubcommand
    {
        private const string CmdName = "workflow";
        private const string CmdDescription = "Common workflows";

        public WorkflowCommand() : base(CmdName, CmdDescription)
        {
        }
    }
}

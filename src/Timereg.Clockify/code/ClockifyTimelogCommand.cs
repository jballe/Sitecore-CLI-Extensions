using System.CommandLine;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;

namespace SitecoreCliExtensions.Timereg.Clockify
{
    internal class ClockifyTimelogCommand : Command, ISubcommand
    {
        private const string CmdName = "timelog";
        private const string CmdDescription = "Manage time logging using Clockify";

        public ClockifyTimelogCommand() : base(CmdName, CmdDescription)
        {
        }
    }
}

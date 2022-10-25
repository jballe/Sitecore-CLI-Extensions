using System;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.TimeEntries
{
    internal static class RegisterTimeEntriesCommandExtensions
    {
        internal static ClockifyTimelogCommand RegisterTimeEntriesSubCommand(this ClockifyTimelogCommand rootCmd,
            IServiceProvider container)
        {
            return rootCmd
                .RegisterStartCommand(container)
                .RegisterStopCommand(container)
                ;
        }

        private static ClockifyTimelogCommand RegisterStartCommand(this ClockifyTimelogCommand rootCmd, IServiceProvider container)
        {
            var cmd = new GenericSubCommand<StopTimeEntryTask, StopTimeEntryArgs>(
                "stop", "Stop timeentry", container);
            rootCmd.AddCommand(cmd);
            return rootCmd;
        }

        private static ClockifyTimelogCommand RegisterStopCommand(this ClockifyTimelogCommand rootCmd, IServiceProvider container)
        {
            var cmd = new GenericSubCommand<StartTimeEntryTask, StartTimeEntryArgs>(
                "start", "Start timeentry", container);
            cmd.AddOption(ArgOptions.WorkspaceArg);
            cmd.AddOption(ArgOptions.ProjectArg);
            cmd.AddOption(ArgOptions.TaskArg);
            cmd.AddOption(ArgOptions.Start);
            cmd.AddOption(ArgOptions.End);

            rootCmd.AddCommand(cmd);
            return rootCmd;
        }
    }
}

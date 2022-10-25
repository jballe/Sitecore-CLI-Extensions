using System;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.Tasks
{
    internal static class RegisterGetTasksCommandExtensions
    {
        internal static ClockifyTimelogCommand RegisterGetTasksSubCommand(this ClockifyTimelogCommand rootCmd, IServiceProvider container)
        {
            var cmd = new GenericSubCommand<GetTasksTask, GetTasksArgs>(
                "tasks", "Fetch tasks", container);
            cmd.AddOption(ArgOptions.WorkspaceArg);
            cmd.AddOption(ArgOptions.ProjectArg);
            cmd.AddOption(ArgOptions.NameOption);
            cmd.AddOption(ArgOptions.Quiet);

            rootCmd.AddCommand(cmd);
            return rootCmd;
        }
    }
}

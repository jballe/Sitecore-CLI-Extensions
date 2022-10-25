using System;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.ConnectionSetup
{
    internal static class RegisterInitializeCommandExtensions
    {
        internal static ClockifyTimelogCommand RegisterInitializeSubCommand(this ClockifyTimelogCommand rootCmd, IServiceProvider container)
        {
            var cmd = new GenericSubCommand<InitializeClockifyTask, InitializeClockifyArgs>(
                InitializeClockifyTask.Name, InitializeClockifyTask.Description, container);
            cmd.AddOption(ArgOptions.WorkspaceArg);
            cmd.AddOption(ArgOptions.ProjectArg);
            cmd.AddOption(ArgOptions.ApiKeyArg);

            rootCmd.AddCommand(cmd);
            return rootCmd;
        }
    }
}

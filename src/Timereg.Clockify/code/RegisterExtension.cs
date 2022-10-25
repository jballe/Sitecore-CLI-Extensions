using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.ConnectionSetup;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.TimeEntries;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Services;

namespace SitecoreCliExtensions.Timereg.Clockify
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<UserConfigService>();
            serviceCollection.AddSingleton<IUserConfigWriter, UserConfigService>();
            serviceCollection.AddSingleton(sp => sp.GetRequiredService<UserConfigService>().GetSync());
            serviceCollection.AddSingleton(ClockifyClientFactory.Create);
        }

        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            yield return new ClockifyTimelogCommand()
                            .RegisterInitializeSubCommand(container)
                            .RegisterGetTasksSubCommand(container)
                            .RegisterTimeEntriesSubCommand(container)
                ;
        }
    }
}

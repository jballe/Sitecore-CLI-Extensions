using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;

namespace SitecoreCliExtensions.Workflows
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
        }

        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            return null;
        }
    }
}

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using Sitecore.DevEx.Client.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Models;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers
{
    public class GenericSubCommand<TTask, TArgs> : SubcommandBase<TTask, TArgs> where TTask : TaskBase<TArgs> where TArgs : TaskOptionsBase
    {
        private readonly IServiceProvider _container;
        private readonly UserConfig _config;

        public GenericSubCommand(string name, string description, IServiceProvider container) : this(name, description, container, container.GetRequiredService<UserConfig>())
        {
        }

        public GenericSubCommand(string name, string description, IServiceProvider container,
            UserConfig config) : base(name, description, container)
        {
            _container = container;
            _config = config;
        }

        protected override Task<int> Handle(TTask task, TArgs args)
        {
            args.Validate();

            if (args is ProjectReferenceArgs refArgs)
            {
                refArgs.Validate(_config);
            }

            return task.Execute(args);
        }
    }
}

using System.Threading.Tasks;
using Sitecore.DevEx.Client.Tasks;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers
{
    public abstract class TaskBase<TArgs> where TArgs : TaskOptionsBase
    {
        public abstract Task<int> Execute(TArgs args);
    }
}

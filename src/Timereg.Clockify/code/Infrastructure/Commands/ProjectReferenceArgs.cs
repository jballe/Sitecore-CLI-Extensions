using System;
using Sitecore.DevEx.Client.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Models;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands
{
    public class ProjectReferenceArgs : TaskOptionsBase, IClockifyProjectReference
    {
        public string Workspace { get; set; }
        public string Project { get; set; }

        public override void Validate()
        {

        }

        public virtual void Validate(UserConfig model)
        {
            ValidateParameterWithDefault(
                nameof(Workspace),
                Workspace,
                model.Workspace,
                () => Workspace = model.Workspace);

            ValidateParameterWithDefault(
                nameof(Project),
                Project,
                model.Project,
                () => Project = model.Project);
        }

        protected void ValidateParameterWithDefault(string nameof, string currentValue, string defaultValue, Action action)
        {
            if (string.IsNullOrEmpty(defaultValue))
            {
                Require(nameof);
            }
            else if (string.IsNullOrEmpty(currentValue))
            {
                action();
            }
        }
    }
}

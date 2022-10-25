namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.Tasks
{
    public class GetTasksArgs : ProjectReferenceArgs
    {
        public string Name { get; set; }

        public bool IncludeInactive { get; set; }

        public bool Quiet { get; set; }
    }
}

namespace SitecoreCliExtensions.Timereg.Clockify.Models
{
    public class UserConfig : IClockifyProjectReference
    {
        public string Workspace { get; set; }
        public string Project { get; set; }
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
    }

    public interface IClockifyProjectReference
    {
        string Workspace { get; set; }
        string Project { get; set; }
    }
}

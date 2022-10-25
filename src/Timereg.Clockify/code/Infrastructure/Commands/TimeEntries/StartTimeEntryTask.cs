using System;
using System.Threading.Tasks;
using Clockify.Net;
using Clockify.Net.Models.TimeEntries;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.TimeEntries
{
    public class StartTimeEntryTask : TaskBase<StartTimeEntryArgs>
    {
        private readonly ClockifyClient _client;

        public StartTimeEntryTask(ClockifyClient client)
        {
            _client = client;
        }

        public override async Task<int> Execute(StartTimeEntryArgs args)
        {
            // var result = await _client.CreateTimeEntryAsync(args.Workspace, new TimeEntryRequest
            // {
            //     WorkspaceId = args.Workspace,
            //     ProjectId = args.Project,
            //     TaskId = args.Task,
            //     Start = args.Start ?? UtcNow(),
            //     End = args.End,
            //     Billable = true,
            // });
            // 
            // if (!result.IsSuccessful)
            // {
            //     Console.WriteLine("Got error:");
            //     Console.Write(result.ErrorMessage);
            //     Console.Write(result.ErrorException);
            //     Console.Write(result.Content);
            //     return -1;
            // }

            Console.WriteLine($"Successfully started timeentry for task id {args.Task}");
            return 0;
        }

        private static DateTimeOffset UtcNow()
        {
            var now = DateTime.UtcNow;
            return new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, new TimeSpan(0));
        }
    }
}

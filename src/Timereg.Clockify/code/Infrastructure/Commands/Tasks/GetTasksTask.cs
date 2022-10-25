using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Clockify.Net;
using Clockify.Net.Models.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.Tasks
{
    public class GetTasksTask : TaskBase<GetTasksArgs>
    {
        private readonly ClockifyClient _client;
        
        public GetTasksTask(ClockifyClient client)
        {
            _client = client;
        }

        public override async Task<int> Execute(GetTasksArgs args)
        {
            var result = await _client.FindAllTasksAsync(
                args.Workspace,
                args.Project,
                !args.IncludeInactive,
                args.Name, 1, 100);

            if (!result.IsSuccessful || result.Data == null)
            {
                Console.Write(result.ErrorMessage);
                return -1;
            }

            if (!result.Data.Any())
            {
                Console.WriteLine("No task found");
                return -1;
            }

            if (args.Quiet)
            {
                await OutputQuiet(result.Data);
            }
            else
            {
                OutputTable(result.Data);
            }

            return 0;
        }

        private async Task OutputQuiet(List<TaskDto> result)
        {
            using var stream = Console.OpenStandardOutput();
            using var writer = new StreamWriter(stream);

            foreach (var item in result)
            {
                await writer.WriteLineAsync(item.Id);
            }
        }

        private void OutputTable(List<TaskDto> result)
        {
            var maxWidth = result.Max(x => x.Id.Length);
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Id.PadRight(maxWidth)}  {item.Name}");
            }
        }

    }
}

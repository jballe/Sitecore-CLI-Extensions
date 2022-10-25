using System;
using System.Reflection;
using System.Threading.Tasks;
using Clockify.Net;
using Clockify.Net.Models;
using RestSharp;
using SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.CliHelpers;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Commands.TimeEntries
{
    public class StopTimeEntryTask : TaskBase<StopTimeEntryArgs>
    {
        private readonly ClockifyClient _client;

        public StopTimeEntryTask(ClockifyClient client)
        {
            _client = client;
        }

        public override async Task<int> Execute(StopTimeEntryArgs args)
        {
            var client = GetRestClient();
            var request = new RestRequest($"workspaces/{args.Workspace}/time-entries", Method.Patch);
            request.AddJsonBody(new
            {
                end = UtcNow(),
            });
            
            var response = await client.ExecuteAsync(request);
            var result = Response.FromRestResponse(response);
            
            if (!result.IsSuccessful)
            {
                Console.Write(result.ErrorMessage);
                return -1;
            }

            return 0;
        }

        private RestClient GetRestClient()
        {
            var field = this._client.GetType().GetField("_client",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (RestClient)field.GetValue(_client);
            return result;
        }

        private static DateTimeOffset UtcNow()
        {
            var now = DateTime.UtcNow;
            return new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, new TimeSpan(0));
        }
    }
}

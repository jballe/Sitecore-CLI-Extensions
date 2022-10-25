using System;
using System.Net;
using System.Reflection;
using Clockify.Net;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using SitecoreCliExtensions.Timereg.Clockify.Models;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Services
{
    internal class ClockifyClientFactory
    {
        public static ClockifyClient Create(IServiceProvider services)
        {
            var config = services.GetRequiredService<UserConfig>();
            var key = Environment.GetEnvironmentVariable("CAPI_KEY") ?? config.ApiKey;
            var client = new ClockifyClient(key, config.ApiUrl);

#if DEBUG
            // System.Diagnostics.Debugger.Launch();
            // var restclient = (RestClient)client.GetType().GetField("_client", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).GetValue(client);
            // var options = (RestClientOptions)restclient.GetType().GetProperty("Options", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(restclient);
            // options.Proxy = new WebProxy("http://127.0.0.1:8888");
#endif
            return client;
        }
    }
}

using System;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Sitecore.Devex.Client.Cli.Extensibility.Tasks;

namespace SitecoreCliExtensions.Timereg.Clockify
{
    internal static class ArgOptions
    {
        public static readonly Option<string> WorkspaceArg = new Option<string>(new[]
        {
            "--workspace",
            "-w"
        }, () => null, "Clockify workspace id");
        public static readonly Option<string> ProjectArg = new Option<string>(new[]
        {
            "--project",
            "-p"
        }, () => null, "Clockify project id");
        public static readonly Option<string> TaskArg = new Option<string>(new[]
        {
            "--task",
            "-t"
        }, () => null, "Clockify task id");
        public static readonly Option<string> NameOption = new Option<string>(new[]
        {
            "--name",
            "-n"
        }, () => null, "Clockify task name");

        public static readonly Option<string> ApiKeyArg = new Option<string>(new[]
        {
            "--api-key",
            "-k"
        }, () => null, "Clockify api key");

        public static readonly Option<DateTimeOffset?> Start = new Option<DateTimeOffset?>(new[]
        {
            "--start",
        }, ParseDate, false, "Start time");

        public static readonly Option<DateTimeOffset?> End = new Option<DateTimeOffset?>(new[]
        {
            "--end",
        }, () => null, "End time");

        public static readonly Option<bool> Quiet = new Option<bool>(new[] { "--quiet", "-q" },
            () => false, "Quiet, only output ids for scripts");

        private static DateTimeOffset? ParseDate(ArgumentResult arg)
        {
            System.Diagnostics.Debugger.Launch();


            var str = arg.Tokens.Single().Value;
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            if (DateTimeOffset.TryParseExact(str, "hh:mm", CultureInfo.CurrentUICulture, DateTimeStyles.AssumeUniversal,
                    out var dateResult))
            {
                return dateResult;
            }

            if (DateTimeOffset.TryParse(str, out dateResult))
            {
                return dateResult;
            }

            Console.WriteLine("Could not parse date value: " + str);
            return null;
        }
    }
}

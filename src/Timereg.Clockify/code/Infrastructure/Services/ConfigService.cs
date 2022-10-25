using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SitecoreCliExtensions.Timereg.Clockify.Models;

namespace SitecoreCliExtensions.Timereg.Clockify.Infrastructure.Services
{
    public interface IUserConfigWriter
    {
        Task Set(UserConfig data);
    }

    public class UserConfigService : IUserConfigWriter
    {
        private readonly string _sitecoreProjectFile = @".sitecore\clockify.json";
        private readonly string _sitecoreUserFile = @".sitecore\clockify.json.user";

        private const string ClockifyDefaultUrl = "https://api.clockify.me/api/v1";

        private static readonly Newtonsoft.Json.JsonSerializerSettings Settings = new()
        {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
                {
                    NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy(),
                },
                DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.IgnoreAndPopulate,
                Formatting = Newtonsoft.Json.Formatting.Indented,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };

        public UserConfig GetSync()
        {
            var task = GetAsync();
            task.Wait();
            return task.Result;
        }

        public async Task<UserConfig> GetAsync()
        {
            var shared = await Read<SharedConfigFileData>(_sitecoreProjectFile);
            var user = await Read<UserConfigFileData>(_sitecoreUserFile);

            var apiKey = new[]
            {
                Environment.GetEnvironmentVariable("CAPI_KEY"),
                user.ApiKey,
            }.FirstOrDefault(x => !string.IsNullOrEmpty(x));

            return new UserConfig
            {
                ApiKey = apiKey,
                ApiUrl = shared.ApiUrl,
                Workspace = shared.Workspace,
                Project = shared.Project,
            };
        }

        public async Task Set(UserConfig data)
        {
            await Save(_sitecoreProjectFile, new SharedConfigFileData
            {
                ApiUrl = data.ApiUrl,
                Workspace = data.Workspace,
                Project = data.Project,
            });
            await Save(_sitecoreUserFile, new UserConfigFileData
            {
                ApiKey = data.ApiKey,
            });
        }

        private static async Task<T> Read<T>(string fileName) where T : new()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), fileName);
            if (!File.Exists(path))
            {
                return new T();
            }

            try
            {
                using var reader = new StreamReader(path);
                var json = await reader.ReadToEndAsync();
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, Settings);
                return obj;
            }
            catch (Exception)
            {
                return new T();
            }
        }

        private static async Task Save<T>(string fileName, T obj)
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), fileName);
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Settings);

            if (json == "{}")
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                return;
            }

            await using var writer = new StreamWriter(path);
            await writer.WriteAsync(json);
        }


        public class SharedConfigFileData
        {
            public string Workspace { get; set; }
            public string Project { get; set; }
            [DefaultValue(ClockifyDefaultUrl)]
            public string ApiUrl { get; set; }
        }

        public class UserConfigFileData
        {
            public string ApiKey { get; set; }
        }
    }
}

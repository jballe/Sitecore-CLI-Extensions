var path = @"C:\Projects\jballe-github\Sitecore-cli-timereg\sample\.sitecore\package-cache\nuget\SitecoreCli.DevEx.Extensions.Timereg.Clockify.1.0.0.2\plugin\SitecoreCli.DevEx.Extensions.Timereg.Clockify.dll";
try
{
    var ctx = new PluginLoadContext(path);
    var assembly = ctx.LoadFromAssemblyPath(path);
    var types = assembly.GetTypes();
    Console.WriteLine("OK :)");
}
catch (System.Reflection.ReflectionTypeLoadException ex)
{
    Console.WriteLine(ex.Message);
}

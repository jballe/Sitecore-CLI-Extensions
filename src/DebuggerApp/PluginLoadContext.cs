using System.Reflection;
using System.Runtime.Loader;

class PluginLoadContext : AssemblyLoadContext
{
    private readonly AssemblyDependencyResolver _resolver;

    public PluginLoadContext(string pluginPath) => this._resolver = new AssemblyDependencyResolver(pluginPath);

    protected override Assembly Load(AssemblyName assemblyName)
    {
        string path = this._resolver.ResolveAssemblyToPath(assemblyName);
        return path != null ? this.LoadFromAssemblyPath(path) : (Assembly)null;
    }

    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        string path = this._resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        return path != null ? this.LoadUnmanagedDllFromPath(path) : IntPtr.Zero;
    }
}
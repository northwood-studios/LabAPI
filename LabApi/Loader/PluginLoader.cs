using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using LabApi.Loader.Features.Misc;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;

namespace LabApi.Loader;

/// <summary>
/// LabAPIs plugin loader.
/// Responsible for loading all the different <see cref="Plugin"/>s.
/// </summary>
public static class PluginLoader
{
    private const string DllSearchPattern = "*.dll";
    
    /// <summary>
    /// Whether or not the <see cref="PluginLoader"/> has been initialized.
    /// </summary>
    public static bool Initialized { get; private set; }

    /// <summary>
    /// The loaded <see cref="Assembly"/> dependencies.
    /// </summary>
    public static HashSet<Assembly> Dependencies { get; } = [];
    
    /// <summary>
    /// The loaded <see cref="Plugin"/>s.
    /// </summary>
    public static Dictionary<Assembly, Plugin> Plugins { get; } = [];
    
    /// <summary>
    /// The enabled <see cref="Plugin"/>s.
    /// </summary>
    public static HashSet<Plugin> EnabledPlugins { get; } = [];
    
    /// <summary>
    /// Initializes the <see cref="PluginLoader"/> and loads all plugins.
    /// </summary>
    public static void Initialize()
    {
        // If the loader has already been initialized, we skip the initialization.
        if (Initialized)
            return;
        
        Initialized = true;
        
        // We first load all the dependencies and store them in the dependencies list
        LoadAllDependencies();
        // Then we load all the plugins and enable them
        LoadAllPlugins();
    }
    
    /// <summary>
    /// Loads all dependencies found inside the <see cref="PathManager.Dependencies"/> directory.
    /// </summary>
    public static void LoadAllDependencies()
    {
        // We load all the dependencies from the dependencies directory
        ServerConsole.AddLog("[LabAPI] [Loader] Loading all dependencies", ConsoleColor.DarkCyan); // Temporary until we have a logger
        LoadDependencies(PathManager.Dependencies.GetFiles(DllSearchPattern));
    }
    
    /// <summary>
    /// Loads dependencies from a collection of files.
    /// </summary>
    /// <param name="files">The collection of assemblies to load.</param>
    public static void LoadDependencies(IEnumerable<FileInfo> files)
    {
        foreach (FileInfo file in files)
        {
            try
            {
                // We load the assembly from the specified file.
                Assembly assembly = Assembly.Load(File.ReadAllBytes(file.FullName));
                
                // And we add the assembly to the dependencies list.
                Dependencies.Add(assembly);
                
                // We finally log that the dependency has been loaded.
                ServerConsole.AddLog($"[LabAPI] [Loader] Successfully loaded {assembly.FullName}", ConsoleColor.Green); // Temporary until we have a logger
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[LabAPI] [Loader] [ERROR] Couldn't load the dependency inside '{file.FullName}'", ConsoleColor.Red);
                ServerConsole.AddLog(e.ToString(), ConsoleColor.Red);
            }
        }
    }

    /// <summary>
    /// Loads all plugins found inside the <see cref="PathManager.Plugins"/> directory.
    /// </summary>
    public static void LoadAllPlugins()
    {
        // First we load all the plugins from the plugins directory
        ServerConsole.AddLog("[LabAPI] [Loader] Loading all plugins", ConsoleColor.DarkCyan); // Temporary until we have a logger
        LoadPlugins(PathManager.Plugins.GetFiles(DllSearchPattern));
        
        // Then we finally enable all the plugins
        ServerConsole.AddLog("[LabAPI] [Loader] Enabling all plugins", ConsoleColor.DarkCyan);
        EnablePlugins(Plugins.Values.OrderBy(plugin => plugin.Priority));
    }
    
    /// <summary>
    /// Loads plugins from a collection of files.
    /// </summary>
    /// <param name="files">The collection of assemblies to load.</param>
    public static void LoadPlugins(IEnumerable<FileInfo> files)
    {
        foreach (FileInfo file in files)
        {
            try
            {
                // We load the assembly from the specified file.
                Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(file.FullName));

                // If the assembly has missing dependencies, we skip it.
                if (AssemblyUtils.HasMissingDependencies(pluginAssembly, file.FullName, out Type[] types)) 
                    continue;
                
                foreach (Type type in types)
                {
                    // We check if the type is derived from Plugin.
                    if (!type.IsSubclassOf(typeof(Plugin))) 
                        continue;

                    // We create an instance of the type and check if it was successfully created.
                    if (Activator.CreateInstance(type) is not Plugin plugin)
                        continue;
                    
                    // In that case, we add the plugin to the plugins list and log that it has been loaded.
                    Plugins.Add(pluginAssembly, plugin);
                    ServerConsole.AddLog($"[LabAPI] [Loader] Successfully loaded {plugin.Name}", ConsoleColor.Green); // Temporary until we have a logger
                }
            }
            catch (Exception e)
            {
                ServerConsole.AddLog($"[LabAPI] [Loader] [ERROR] Couldn't load the plugin inside '{file.FullName}'", ConsoleColor.Red);
                ServerConsole.AddLog(e.ToString(), ConsoleColor.Red);
            }
        }
    }

    /// <summary>
    /// Enables a collection of <see cref="Plugin"/>s.
    /// </summary>
    /// <param name="plugins">The sorted collection of <see cref="Plugin"/>s.</param>
    public static void EnablePlugins(IEnumerable<Plugin> plugins)
    {
        foreach (Plugin plugin in plugins)
        {
            // Here we can load the config
            // ConfigurationManager.LoadConfig(plugin.Config);
            
            // Here we should check if the plugin is disabled in the config
            // if (!plugin.Config.IsEnabled)
            //     continue;
            
            // We finally enable the plugin
            EnablePlugin(plugin);
        }
    }

    public static void EnablePlugin(Plugin plugin)
    {
        try
        {
            // We register all the plugin commands
            // CommandManager.RegisterCommands(plugin);

            // We enable the plugin if it is not disabled
            plugin.Enable();

            // We add the plugin to the enabled plugins list
            EnabledPlugins.Add(plugin);

            // We finally log that the plugin has been enabled
            ServerConsole.AddLog($"[LabAPI] [Loader] Successfully enabled {plugin}", ConsoleColor.Green); // Temporary until we have a logger
        }
        catch (Exception e)
        {
            ServerConsole.AddLog($"[LabAPI] [Loader] [ERROR] Couldn't enable the plugin {plugin}", ConsoleColor.Red);
            ServerConsole.AddLog(e.ToString(), ConsoleColor.Red);
        }
    }
}
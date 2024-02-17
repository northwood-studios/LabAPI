using System;
using System.IO;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Configuration;
using Serialization;

namespace LabApi.Loader;

/// <summary>
/// LabAPIs plugin configuration loader.
/// Responsible for loading all the different <see cref="Plugin"/> configs.
/// </summary>
public static class ConfigurationLoader
{
    private const string ConfigFileName = "config.yml";
    
    /// <summary>
    /// Loads the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to load the configuration for.</param>
    /// <returns>Whether or not the configuration was successfully loaded.</returns>
    public static bool TryLoadConfig(this Plugin plugin)
    {
        // We retrieve the path of the configuration file.
        string path = plugin.GetConfigPath();
        
        // If the configuration file exists, we load it and return whether or not it was successfully loaded.
        if (File.Exists(path))
        {
            // We try to read the configuration file.
            if (!plugin.TryReadConfig(out IConfig config))
            {
                // We log the error and return false to indicate that the configuration wasn't successfully loaded.
                return false;
            }
            
            // We update the configuration of the plugin.
            plugin.Config = config;
        }
        
        // We save the configuration to update new properties and return whether or not it was successfully saved.
        return plugin.TrySaveConfig();
    }
    
    /// <summary>
    /// Tries to save the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to save the configuration for.</param>
    /// <returns>Whether or not the configuration was successfully saved.</returns>
    public static bool TrySaveConfig(this Plugin plugin)
    {
        try
        {
            // We retrieve the path of the configuration file.
            string path = plugin.GetConfigPath();

            // We serialize the configuration.
            string serializedConfig = YamlParser.Serializer.Serialize(plugin.Config);

            // We finally write the serialized configuration to the file and return whether or not it was successful.
            File.WriteAllText(path, serializedConfig);

            // We return true to indicate that the configuration was successfully saved.
            return true;
        }
        catch (Exception e)
        {
            // We log the error and return false to indicate that the configuration wasn't successfully saved.
            ServerConsole.AddLog($"[LabAPI] [Loader] [ERROR] Couldn't save the configuration of the plugin {plugin}", ConsoleColor.Red);
            ServerConsole.AddLog(e.ToString(), ConsoleColor.Red);
            return false;
        }
    }

    /// <summary>
    /// We try to read the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to read the configuration for.</param>
    /// <param name="config">The configuration of the specified <see cref="Plugin"/>.</param>
    /// <returns>Whether or not the configuration was successfully read.</returns>
    public static bool TryReadConfig(this Plugin plugin, out IConfig config)
    {
        config = default;
        
        try
        {
            // We retrieve the path of the configuration file.
            string path = plugin.GetConfigPath();

            // If the configuration file doesn't exist, we return false to indicate that the configuration wasn't successfully read.
            if (!File.Exists(path))
                return false;
            
            // We read the configuration file.
            string serializedConfig = File.ReadAllText(path);

            // We deserialize the configuration and return whether or not it was successful.
            config = YamlParser.Deserializer.Deserialize<IConfig>(serializedConfig);
            return true;
        }
        catch (Exception e)
        {
            // We log the error and return false to indicate that the configuration wasn't successfully read.
            ServerConsole.AddLog($"[LabAPI] [Loader] [ERROR] Couldn't read the configuration of the plugin {plugin}", ConsoleColor.Red);
            ServerConsole.AddLog(e.ToString(), ConsoleColor.Red);
            return false;
        }
    }
    
    /// <summary>
    /// Gets the directory of the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to get the configuration directory for.</param>
    /// <returns>The directory of the configuration of the specified <see cref="Plugin"/>.</returns>
    public static DirectoryInfo GetConfigDirectory(this Plugin plugin)
    {
        // We create the directory for the plugin if it doesn't exist and return it.
        return PathManager.Configs.CreateSubdirectory(plugin.Name);
    }
    
    /// <summary>
    /// Gets the path of the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to get the configuration path for.</param>
    /// <returns>The path of the configuration of the specified <see cref="Plugin"/>.</returns>
    public static string GetConfigPath(this Plugin plugin)
    {
        // We create the directory for the plugin if it doesn't exist.
        DirectoryInfo directory = plugin.GetConfigDirectory();
        
        // We retrieve the path of the configuration file.
        return Path.Combine(directory.FullName, ConfigFileName);
    }
}
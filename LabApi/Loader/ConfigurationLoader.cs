using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using LabApi.Loader.Features.Paths;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Configuration;
using LabApi.Loader.Features.Yaml;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace LabApi.Loader;

/// <summary>
/// LabAPIs plugin configuration loader.
/// Responsible for loading all the different <see cref="Plugin"/> configs.
/// </summary>
public static class ConfigurationLoader
{
    private const string LoggerPrefix = "[CONFIG_LOADER]";
    private const string PropertiesFileName = "properties.yml";

    /// <summary>
    /// Tries to save the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to save the configuration for.</param>
    /// <param name="config">The configuration to save.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to save.</typeparam>
    /// <returns>Whether the configuration was successfully saved.</returns>
    public static bool TrySaveConfig<TConfig>(this Plugin plugin, TConfig config, string fileName, bool isGlobal = false)
        where TConfig : class, new()
    {
        try
        {
            // We retrieve the path of the configuration file.
            string path = plugin.GetConfigPath(fileName, isGlobal);

            // We serialize the configuration.
            string serializedConfig = YamlConfigParser.Serializer.Serialize(config);

            // We finally write the serialized configuration to the file and return whether it was successful.
            File.WriteAllText(path, serializedConfig);

            // We return true to indicate that the configuration was successfully saved.
            return true;
        }
        catch (Exception e)
        {
            // We log the error and return false to indicate that the configuration wasn't successfully saved.
            Logger.Error($"{LoggerPrefix} Couldn't save the configuration of the plugin {plugin}");
            Logger.Error(e);
            return false;
        }
    }

    /// <summary>
    /// Tries to read the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to read the configuration for.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="config">The read configuration of the specified <see cref="Plugin"/> if successful, otherwise <see langword="null"/>.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to read.</typeparam>
    /// <returns>Whether the configuration was successfully read.</returns>
    public static bool TryReadConfig<TConfig>(this Plugin plugin, string fileName, [NotNullWhen(true)] out TConfig? config, bool isGlobal = false)
        where TConfig : class, new()
    {
        config = null;

        try
        {
            // We retrieve the path of the configuration file.
            string path = plugin.GetConfigPath(fileName, isGlobal);

            // If the configuration file doesn't exist, we return false to indicate that the configuration wasn't successfully read.
            if (!File.Exists(path))
            {
                return false;
            }

            // We read the configuration file.
            string serializedConfig = File.ReadAllText(path);

            // We deserialize the configuration and return whether it was successful.
            config = YamlConfigParser.Deserializer.Deserialize<TConfig>(serializedConfig);
            return true;
        }
        catch (Exception e)
        {
            // We log the error and return false to indicate that the configuration wasn't successfully read.
            Logger.Error($"{LoggerPrefix} Couldn't read the configuration of the plugin {plugin}");
            Logger.Error(e);
            return false;
        }
    }

    /// <summary>
    /// Tries to read the configuration of the specified <see cref="Plugin"/> and creates a default instance if it doesn't exist.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to load the configuration for.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="config">The loaded configuration of the specified <see cref="Plugin"/> if successful, otherwise <see langword="null"/>.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to load.</typeparam>
    /// <returns>Whether the configuration was successfully loaded.</returns>
    public static bool TryLoadConfig<TConfig>(this Plugin plugin, string fileName, [NotNullWhen(true)] out TConfig? config, bool isGlobal = false)
        where TConfig : class, new()
    {
        // We retrieve the path of the configuration file.
        string path = plugin.GetConfigPath(fileName, isGlobal);

        // We check if the configuration file doesn't exist to prevent resetting it if any error occurs.
        if (!File.Exists(path))
        {
            // We try to create a default instance of the configuration.
            if (plugin.TryCreateDefaultConfig(out config))
            {
                // We save the new configuration.
                return plugin.TrySaveConfig(config, fileName, isGlobal);
            }
        }

        // We try to read the configuration from its file.
        else if (plugin.TryReadConfig(fileName, out config, isGlobal))
        {
            // We save the configuration to update new properties and return whether it was successfully saved.
            return plugin.TrySaveConfig(config, fileName, isGlobal);
        }

        // We return false to indicate that the configuration wasn't successfully loaded.
        return false;
    }

    /// <summary>
    /// Saves the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to save the configuration for.</param>
    /// <param name="config">The configuration to save.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to save.</typeparam>
    public static void SaveConfig<TConfig>(this Plugin plugin, TConfig config, string fileName, bool isGlobal = false)
        where TConfig : class, new() => plugin.TrySaveConfig(config, fileName, isGlobal);

    /// <summary>
    /// Reads the configuration of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to read the configuration for.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to read.</typeparam>
    /// <returns>The read configuration of the specified <see cref="Plugin"/> if successful, otherwise <see langword="null"/>.</returns>
    public static TConfig? ReadConfig<TConfig>(this Plugin plugin, string fileName, bool isGlobal = false)
        where TConfig : class, new() => plugin.TryReadConfig(fileName, out TConfig? config, isGlobal) ? config : null;

    /// <summary>
    /// Reads the configuration of the specified <see cref="Plugin"/> and creates a default instance if it doesn't exist.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to load the configuration for.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <typeparam name="TConfig">The type of the configuration to load.</typeparam>
    /// <returns>The loaded configuration of the specified <see cref="Plugin"/> if successful, otherwise <see langword="null"/>.</returns>
    public static TConfig? LoadConfig<TConfig>(this Plugin plugin, string fileName, bool isGlobal = false)
        where TConfig : class, new() => plugin.TryLoadConfig(fileName, out TConfig? config, isGlobal) ? config : null;

    /// <summary>
    /// Tries to create a default instance of the specified configuration.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to create the default configuration for.</param>
    /// <param name="config">The default instance of the configuration if successful, otherwise <see langword="null"/>.</param>
    /// <typeparam name="TConfig">The type of the configuration to create.</typeparam>
    /// <returns>Whether the configuration was successfully created.</returns>
    public static bool TryCreateDefaultConfig<TConfig>(this Plugin plugin, [NotNullWhen(true)] out TConfig? config)
        where TConfig : class, new()
    {
        config = null;

        try
        {
            // We create a default instance of the configuration and return true.
            config = Activator.CreateInstance<TConfig>();
            return true;
        }
        catch (Exception e)
        {
            // We log the error and return false to indicate that the configuration wasn't successfully loaded.
            Logger.Error($"{LoggerPrefix} Couldn't create a default instance of the class {typeof(TConfig)} of the plugin {plugin}");
            Logger.Error(e);
            return false;
        }
    }

    /// <summary>
    /// Gets the configuration directory for a plugin, considering whether it's global or per-port.
    /// </summary>
    /// <param name="plugin">The plugin to gets its config directory.</param>
    /// <param name="isGlobal">Whether the directory is for global configs or per-port ones.</param>
    /// <returns>The <see cref="DirectoryInfo"/> of the plugin.</returns>
    public static DirectoryInfo GetConfigDirectory(this Plugin plugin, bool isGlobal = false)
    {
        DirectoryInfo baseDir = PathManager.Configs;
        baseDir = baseDir.CreateSubdirectory(isGlobal ? "global" : PluginLoader.ResolvePath(PluginLoader.Config.ConfigPath));
        return baseDir.CreateSubdirectory(plugin.Name);
    }

    /// <summary>
    /// Gets the path of the configuration file for the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to get the configuration path for.</param>
    /// <param name="fileName">The name of the configuration file.</param>
    /// <param name="isGlobal">Whether the configuration is global or per-port.</param>
    /// <returns>The path of the configuration of the specified <see cref="Plugin"/>.</returns>
    public static string GetConfigPath(this Plugin plugin, string fileName, bool isGlobal = false)
    {
        // We retrieve the directory of the configuration of the plugin.
        DirectoryInfo directory = plugin.GetConfigDirectory(isGlobal);

        // We check if the file name doesn't end with .yml or .yaml and add it if it doesn't.
        if (!fileName.EndsWith(".yml", StringComparison.InvariantCultureIgnoreCase) && !fileName.EndsWith(".yaml", StringComparison.InvariantCultureIgnoreCase))
        {
            fileName += ".yml";
        }

        // We return the path of the configuration file.
        return Path.Combine(directory.FullName, fileName);
    }

    /// <summary>
    /// Tries to load the properties of the specified <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to load the properties for.</param>
    /// <returns>Whether the properties were successfully loaded.</returns>
    public static bool TryLoadProperties(this Plugin plugin)
    {
        // We try to load the properties of the plugin and return them.
        if (!plugin.TryLoadConfig(PropertiesFileName, out Properties? properties))
        {
            return false;
        }

        // We set the properties of the plugin.
        plugin.Properties = properties;
        return true;
    }
}
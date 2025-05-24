﻿using LabApi.Features.Console;

namespace LabApi.Loader.Features.Plugins;

/// <summary>
/// Represents a plugin which can be loaded by the <see cref="PluginLoader"/>.
/// It also accepts a configuration file as a generic type.
/// </summary>
/// <typeparam name="TConfig">The configuration of the <see cref="Plugin"/>.</typeparam>
public abstract class Plugin<TConfig> : Plugin
    where TConfig : class, new()
{
    /// <summary>
    /// The configuration of the <see cref="Plugin"/>.
    /// This will never be null once <see cref="Plugin.Enable()"/> has been called.
    /// Will revert to default values if loading failed.
    /// </summary>
    public TConfig Config { get; set; } = null!;

    /// <summary>
    /// The file name of the configuration file.
    /// </summary>
    public virtual string ConfigFileName { get; set; } = "config.yml";

    /// <inheritdoc/>
    public override void LoadConfigs()
    {
        // This is a more beginner-friendly approach
        // as it defaults to the default values if the config file broken.
        if (!this.TryLoadConfig(ConfigFileName, out TConfig? config))
        {
            Logger.Warn("Failed to load the configuration file, using default values.");
            config = new TConfig();
        }

        // Then we set the configuration to the loaded one or the default one.
        Config = config;
    }

    /// <summary>
    /// Saves the configuration of the <see cref="Plugin"/> to its configuration file.
    /// </summary>
    public void SaveConfig()
    {
        if (Config == null!)
            Logger.Warn($"Saving the configuration file for {Name}, but the configuration is null (has it loaded yet?). Writing default values.");

        // We directly use SaveConfig(T, name) to save the configuration.
        this.SaveConfig(Config ?? new TConfig(), ConfigFileName);
    }
}
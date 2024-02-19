namespace LabApi.Loader.Features.Plugins;

/// <summary>
/// Represents a plugin which can be loaded by the <see cref="PluginLoader"/>.
/// It also accepts a configuration file as a generic type.
/// </summary>
/// <typeparam name="TConfig">The configuration of the <see cref="Plugin"/>.</typeparam>
public abstract class Plugin<TConfig> : Plugin
{
    /// <summary>
    /// The configuration of the <see cref="Plugin"/>.
    /// </summary>
    public TConfig Config { get; set; }

    /// <summary>
    /// The file name of the configuration file.
    /// </summary>
    public virtual string ConfigFileName { get; set; } = "config.yml";

    /// <inheritdoc/>
    public override void LoadConfigs()
    {
        Config = this.LoadConfig<TConfig>(ConfigFileName);
    }
}
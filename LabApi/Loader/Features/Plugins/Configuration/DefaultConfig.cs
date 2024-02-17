namespace LabApi.Loader.Features.Plugins.Configuration;

/// <summary>
/// The default <see cref="IConfig"/> implementation.
/// Plugins that use this will be enabled by default.
/// </summary>
public class DefaultConfig : IConfig
{
    /// <summary>
    /// Creates a new instance of <see cref="DefaultConfig"/>.
    /// </summary>
    public static DefaultConfig Create() => new ();
    
    /// <inheritdoc/>
    public bool IsEnabled { get; set; } = true;
}
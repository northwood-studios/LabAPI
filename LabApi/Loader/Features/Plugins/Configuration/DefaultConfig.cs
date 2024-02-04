namespace LabApi.Loader.Features.Plugins.Configuration;

/// <summary>
/// The default <see cref="IConfig"/> implementation.
/// Plugins that use this will be enabled by default.
/// </summary>
public class DefaultConfig : IConfig
{
    /// <summary>
    /// The default constructor for <see cref="DefaultConfig"/>.
    /// Private to prevent instantiation outside of this class.
    /// </summary>
    private DefaultConfig() {}
    
    /// <summary>
    /// Creates a new instance of <see cref="DefaultConfig"/>.
    /// </summary>
    public static DefaultConfig Create() => new ();
    
    /// <inheritdoc/>
    public bool IsEnabled { get; set; } = true;
}
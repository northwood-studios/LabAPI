using System.ComponentModel;

namespace LabApi.Loader.Features.Plugins.Configuration;

/// <summary>
/// The properties of the <see cref="Plugin"/>.
/// </summary>
public class Properties
{
    /// <summary>
    /// Creates a new instance of the <see cref="Properties"/> class.
    /// </summary>
    /// <returns>A new instance of the <see cref="Properties"/> class.</returns>
    public static Properties CreateDefault() => new();

    /// <summary>
    /// Whether the <see cref="Plugin"/> is enabled.
    /// </summary>
    [Description("Whether or not the plugin is enabled.")]
    public bool IsEnabled { get; set; } = true;
}
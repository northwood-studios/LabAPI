using System.ComponentModel;

namespace LabApi.Loader.Features.Plugins.Configuration;

/// <summary>
/// The configuration of the <see cref="Plugin"/>.
/// </summary>
public interface IConfig
{
    /// <summary>
    /// Whether or not the <see cref="Plugin"/> is enabled.
    /// </summary>
    [Description("Whether or not the plugin is enabled.")]
    public bool IsEnabled { get; set; }
}
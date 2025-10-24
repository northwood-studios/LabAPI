using LabApi.Loader.Features.Plugins.Enums;
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

    /// <summary>
    /// Whether to allow loading the plugin even if it was built for a different major version of LabAPI.
    /// </summary>
    [Description("""Whether to allow loading the plugin even if it was built for a different major version of LabAPI. "Default" = use port-specific config; "True"/"Enabled" = load if unsupported; "False"/"Disabled" = do not load if unsupported""")]
    public OptionalBoolean UnsupportedLoading { get; set; } = OptionalBoolean.Default;
}

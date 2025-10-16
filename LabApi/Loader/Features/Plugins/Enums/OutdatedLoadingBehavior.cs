using LabApi.Loader.Features.Plugins.Configuration;

namespace LabApi.Loader.Features.Plugins.Enums;

/// <summary>
/// Represents values whether a specific unsupported <see cref="Plugin"/> should be loaded.
/// </summary>
/// <seealso cref="Properties.UnsupportedLoading"/>
public enum OutdatedLoadingBehavior
{
    /// <summary>
    /// Uses the setting in the port-specific LabAPI config.
    /// </summary>
    /// <seealso cref="LabApi.Loader.Features.Configuration.LabApiConfig.LoadUnsupportedPlugins"/>
    Default,

    /// <summary>
    /// Overrides the port-specific setting to load the plugin even if it's unsupported.
    /// </summary>
    True,

    /// <summary>
    /// Overrides the port-specific setting not to load the plugin if it's unsupported.
    /// </summary>
    False,

    /// <inheritdoc cref="True" />
    Enabled = True,

    /// <inheritdoc cref="False" />
    Disabled = False,
}

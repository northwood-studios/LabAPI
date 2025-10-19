namespace LabApi.Loader.Features.Plugins.Enums;

/// <summary>
/// Represents values that specify whether a feature should be activated.
/// </summary>
public enum OptionalBoolean
{
    /// <summary>
    /// Uses default behavior, specified in the feature's implementation or in a broader scope.
    /// </summary>
    Default = -1,

    /// <summary>
    /// Overrides default behavior to disable the feature.
    /// </summary>
    False = 0,

    /// <summary>
    /// Overrides default behavior to enable the feature.
    /// </summary>
    True = 1,

    /// <inheritdoc cref="True" />
    Enabled = True,

    /// <inheritdoc cref="False" />
    Disabled = False,
}

namespace LabApi.Loader.Constants;

/// <summary>
/// Represents priority levels as constant byte values. (Lower values indicate higher priority)
/// Used throughout the API to determine the order of execution order of various features.
/// </summary>
/// <remarks>
/// You can use a value lower than <see cref="Priority.Highest"/> or higher than <see cref="Priority.Lowest"/> to create custom priority levels.
/// </remarks>
public static class Priority
{
    /// <summary>
    /// Represents the highest priority level.
    /// </summary>
    public const byte Highest = 64;

    /// <summary>
    /// Represents a high priority level.
    /// </summary>
    public const byte High = 96;

    /// <summary>
    /// Represents a medium priority level.
    /// </summary>
    public const byte Medium = 128;

    /// <summary>
    /// Represents a low priority level.
    /// </summary>
    public const byte Low = 160;

    /// <summary>
    /// Represents the lowest priority level.
    /// </summary>
    public const byte Lowest = 192;
}
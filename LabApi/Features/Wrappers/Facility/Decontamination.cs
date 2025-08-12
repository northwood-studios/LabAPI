using static LightContainmentZoneDecontamination.DecontaminationController;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Static wrapper for LCZ decontamination.
/// </summary>
public static class Decontamination
{
    /// <summary>
    /// Gets or sets the decontamination status.
    /// </summary>
    public static DecontaminationStatus Status
    {
        get => Singleton.DecontaminationOverride;
        set => Singleton.DecontaminationOverride = value;
    }

    /// <summary>
    /// Gets whether the LCZ is currently being decontaminated.
    /// </summary>
    public static bool IsDecontaminating => Singleton.IsDecontaminating;

    /// <summary>
    /// Gets the current server time since round has started plus the <see cref="Offset"/>.
    /// </summary>
    public static double ServerTime => GetServerTime;

    /// <summary>
    /// Gets the network time at which round has started. Value of -1 means the round hasnt started yet.
    /// </summary>
    public static double RoundStartTime => Singleton.RoundStartTime;

    /// <summary>
    /// Gets or sets the offset of the decontamination timer in seconds.
    /// Positive values decrease the timer and negative extend it.<br/>
    /// Setting the offset will ignore any decontamination announcements for clients for 1 second.
    /// </summary>
    public static float Offset
    {
        get => Singleton.TimeOffset;
        set => Singleton.TimeOffset = value;
    }

    /// <summary>
    /// Gets or sets the text of the wall elevator display in HCZ part of the elevators.
    /// Setting this to <see cref="string.Empty"/> or <see langword="null"/> will reset both texts to the default one.
    /// </summary>s
    public static string ElevatorsText
    {
        get => Singleton.ElevatorsLockedText;
        set => Singleton.ElevatorsLockedText = value;
    }
}
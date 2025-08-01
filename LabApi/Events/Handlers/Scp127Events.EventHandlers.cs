using LabApi.Events.Arguments.Scp127Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to the SCP-127.
/// </summary>
public static partial class Scp127Events
{
    /// <summary>
    /// Gets called when the SCP-127 is gaining experience.
    /// </summary>
    public static event LabEventHandler<Scp127GainingExperienceEventArgs>? GainingExperience;

    /// <summary>
    /// Gets called when the SCP-127 is gained experience.
    /// </summary>
    public static event LabEventHandler<Scp127GainExperienceEventArgs>? GainExperience;

    /// <summary>
    /// Gets called when the SCP-127 is levelling up.
    /// </summary>
    public static event LabEventHandler<Scp127LevellingUpEventArgs>? LevellingUp;

    /// <summary>
    /// Gets called when the SCP-127 level up.
    /// </summary>
    public static event LabEventHandler<Scp127LevelUpEventArgs>? LevelUp;

    /// <summary>
    /// Gets called when SCP-127 is about to play a voiceline.
    /// </summary>
    public static event LabEventHandler<Scp127TalkingEventArgs>? Talking;

    /// <summary>
    /// Gets called when SCP-127 played a voiceline.
    /// </summary>
    public static event LabEventHandler<Scp127TalkedEventArgs>? Talked;
}

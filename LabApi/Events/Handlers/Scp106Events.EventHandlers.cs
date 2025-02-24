using LabApi.Events.Arguments.Scp106Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-106.
/// </summary>
public static partial class Scp106Events
{
    /// <summary>
    /// Gets called when SCP-106 changes its stalk mode.
    /// </summary>
    public static event LabEventHandler<Scp106ChangingStalkModeEventArgs>? ChangingStalkMode;

    /// <summary>
    /// Gets called when SCP-106 has changed its stalk mode.
    /// </summary>
    public static event LabEventHandler<Scp106ChangedStalkModeEventArgs>? ChangedStalkMode;

    /// <summary>
    /// Gets called when SCP-106 is changing its vigor.
    /// </summary>
    public static event LabEventHandler<Scp106ChangingVigorEventArgs>? ChangingVigor;

    /// <summary>
    /// Gets called when SCP-106 has changed its vigor.
    /// </summary>
    public static event LabEventHandler<Scp106ChangedVigorEventArgs>? ChangedVigor;

    /// <summary>
    /// Gets called when SCP-106 uses the Hunter Atlas.
    /// </summary>
    public static event LabEventHandler<Scp106UsedHunterAtlasEventArgs>? UsedHunterAtlas;

    /// <summary>
    /// Gets called when SCP-106 is using the Hunter Atlas.
    /// </summary>
    public static event LabEventHandler<Scp106UsingHunterAtlasEventArgs>? UsingHunterAtlas;

    /// <summary>
    /// Gets called when SCP-106 is changing its submersion status.
    /// </summary>
    public static event LabEventHandler<Scp106ChangingSubmersionStatusEventArgs>? ChangingSubmersionStatus;

    /// <summary>
    /// Gets called when SCP-106 has changed its submersion status.
    /// </summary>
    public static event LabEventHandler<Scp106ChangedSubmersionStatusEventArgs>? ChangedSubmersionStatus;

    /// <summary>
    /// Gets called when SCP-106 is teleporting a player.
    /// </summary>
    public static event LabEventHandler<Scp106TeleportingPlayerEvent>? TeleportingPlayer;

    /// <summary>
    /// Gets called when SCP-106 has teleported a player.
    /// </summary>
    public static event LabEventHandler<Scp106TeleportedPlayerEvent>? TeleportedPlayer;
}
using LabApi.Events.Arguments.Scp914Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-914.
/// </summary>
public static partial class Scp914Events
{
    /// <summary>
    /// Gets called when SCP-914 is being activated.
    /// </summary>
    public static event LabEventHandler<Scp914ActivatingEventArgs>? Activating;

    /// <summary>
    /// Gets called when SCP-914 is activated.
    /// </summary>
    public static event LabEventHandler<Scp914ActivatedEventArgs>? Activated;

    /// <summary>
    /// Gets called when SCP-914's knob is changing.
    /// </summary>
    public static event LabEventHandler<Scp914KnobChangingEventArgs>? KnobChanging;

    /// <summary>
    /// Gets called when SCP-914's knob has changed.
    /// </summary>
    public static event LabEventHandler<Scp914KnobChangedEventArgs>? KnobChanged;

    /// <summary>
    /// Gets called when SCP-914 is processing a pickup.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessingPickupEventArgs>? ProcessingPickup;

    /// <summary>
    /// Gets called when SCP-914 has processed a pickup.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessedPickupEventArgs>? ProcessedPickup;

    /// <summary>
    /// Gets called when SCP-914 is processing a player.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessingPlayerEventArgs>? ProcessingPlayer;

    /// <summary>
    /// Gets called when SCP-914 has processed a player.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessedPlayerEventArgs>? ProcessedPlayer;

    /// <summary>
    /// Gets called when SCP-914 is processing an inventory item.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessingInventoryItemEventArgs>? ProcessingInventoryItem;

    /// <summary>
    /// Gets called when SCP-914 has processed an inventory item.
    /// </summary>
    public static event LabEventHandler<Scp914ProcessedInventoryItemEventArgs>? ProcessedInventoryItem;
}
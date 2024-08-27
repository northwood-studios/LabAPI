using LabApi.Events.Arguments.Scp914Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp914Events
{
    /// <summary>
    /// Invokes the <see cref="Activating"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ActivatingEventArgs"/> instance containing the event data.</param>
    public static void OnActivating(Scp914ActivatingEventArgs args) => Activating.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Activated"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ActivatedEventArgs"/> instance containing the event data.</param>
    public static void OnActivated(Scp914ActivatedEventArgs args) => Activated.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="KnobChanging"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914KnobChangingEventArgs"/> instance containing the event data.</param>
    public static void OnKnobChanging(Scp914KnobChangingEventArgs args) => KnobChanging.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="KnobChanged"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914KnobChangedEventArgs"/> instance containing the event data.</param>
    public static void OnKnobChanged(Scp914KnobChangedEventArgs args) => KnobChanged.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessingPlayer"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessingPlayerEventArgs"/> instance containing the event data.</param>
    public static void OnProcessingPlayer(Scp914ProcessingPlayerEventArgs args) => ProcessingPlayer.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessedPlayer"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessedPlayerEventArgs"/> instance containing the event data.</param>
    public static void OnProcessedPlayer(Scp914ProcessedPlayerEventArgs args) => ProcessedPlayer.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessingPickup"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessingPickupEventArgs"/> instance containing the event data.</param>
    public static void OnProcessingPickup(Scp914ProcessingPickupEventArgs args) => ProcessingPickup.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessedPickup"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessedPickupEventArgs"/> instance containing the event data.</param>
    public static void OnProcessedPickup(Scp914ProcessedPickupEventArgs args) => ProcessedPickup.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessingInventoryItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessingInventoryItemEventArgs"/> instance containing the event data.</param>
    public static void OnProcessingInventoryItem(Scp914ProcessingInventoryItemEventArgs args) => ProcessingInventoryItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ProcessedInventoryItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp914ProcessedInventoryItemEventArgs"/> instance containing the event data.</param>
    public static void OnProcessedInventoryItem(Scp914ProcessedInventoryItemEventArgs args) => ProcessedInventoryItem.InvokeEvent(args);
}
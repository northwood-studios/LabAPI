using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TriggeringTesla"/> event.
/// </summary>
public class PlayerTriggeringTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTriggeringTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is causing a tesla gate to trigger.</param>
    /// <param name="gate">The tesla gate that is triggering.</param>
    public PlayerTriggeringTeslaEventArgs(ReferenceHub hub, TeslaGate gate)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Tesla = Tesla.Get(gate);
    }

    /// <summary>
    /// Gets the player who is causing a tesla gate to trigger.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Tesla Tesla { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
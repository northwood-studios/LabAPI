using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.IdlingTesla"/> event.
/// </summary>
public class PlayerIdlingTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerIdlingTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is causing a tesla gate to idle.</param>
    /// <param name="gate">The tesla gate that is idling.</param>
    public PlayerIdlingTeslaEventArgs(ReferenceHub hub, TeslaGate gate)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Tesla = Tesla.Get(gate);
    }

    /// <summary>
    /// Gets the player who is causing a tesla gate to idle.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Tesla Tesla { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
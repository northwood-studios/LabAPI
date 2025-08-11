using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.IdledTesla"/> event.
/// </summary>
public class PlayerIdledTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerIdledTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who caused the tesla gate to idle.</param>
    /// <param name="gate">The tesla gate that is Idled.</param>
    public PlayerIdledTeslaEventArgs(ReferenceHub hub, TeslaGate gate)
    {
        Player = Player.Get(hub);
        Tesla = Tesla.Get(gate);
    }

    /// <summary>
    /// Gets the player who is causing a tesla gate to idle.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Tesla Tesla { get; }
}
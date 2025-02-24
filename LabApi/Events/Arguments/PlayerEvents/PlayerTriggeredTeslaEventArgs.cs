using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.TriggeredTesla"/> event.
/// </summary>
public class PlayerTriggeredTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerTriggeredTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who caused the tesla gate to trigger.</param>
    /// <param name="gate">The tesla gate that is triggered.</param>
    public PlayerTriggeredTeslaEventArgs(ReferenceHub hub, TeslaGate gate)
    {
        Player = Player.Get(hub);
        Tesla = Tesla.Get(gate);
    }

    /// <summary>
    /// Gets the player who is causing a tesla gate to trigger.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Tesla Tesla { get; }
}
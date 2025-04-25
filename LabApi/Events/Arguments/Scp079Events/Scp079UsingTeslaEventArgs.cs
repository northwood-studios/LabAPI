using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.UsingTesla"/> event.
/// </summary>
public class Scp079UsingTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079UsingTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="tesla">The affected tesla instance.</param>
    public Scp079UsingTeslaEventArgs(ReferenceHub player, TeslaGate tesla)
    {
        Player = Player.Get(player);
        Tesla = Tesla.Get(tesla);
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public Tesla Tesla { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}

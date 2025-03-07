using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.UsedTesla"/> event.
/// </summary>
public class Scp079UsedTeslaEventArgs : EventArgs, IPlayerEvent, ITeslaEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079UsedTeslaEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="tesla">The affected tesla instance.</param>
    public Scp079UsedTeslaEventArgs(ReferenceHub player, TeslaGate tesla)
    {
        Player = Player.Get(player);
        Tesla = Tesla.Get(tesla);
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }
    
    /// <inheritdoc />
    public Tesla Tesla { get; }
}

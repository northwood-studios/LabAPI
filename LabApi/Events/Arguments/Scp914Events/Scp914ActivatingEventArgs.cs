using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when SCP-914 is being activated.
/// </summary>
public class Scp914ActivatingEventArgs : EventArgs, ICancellableEvent, IScp914Event, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ActivatingEventArgs"/> class.
    /// </summary>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="player">The player that is activating SCP-914.</param>
    public Scp914ActivatingEventArgs(Scp914KnobSetting knobSetting, Player player)
    {
        IsAllowed = true;
        KnobSetting = knobSetting;
        Player = player;
    }
    
    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; set; }
    
    /// <summary>
    /// The player that is activating SCP-914.
    /// </summary>
    public Player Player { get; }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
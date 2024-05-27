using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when the knob of SCP-914 is changing.
/// </summary>
public class Scp914KnobChangingEventArgs : EventArgs, ICancellableEvent, IScp914Event, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914KnobChangingEventArgs"/> class.
    /// </summary>
    /// <param name="oldKnobSetting">The old knob setting of SCP-914.</param>
    /// <param name="knobSetting">The new knob setting of SCP-914.</param>
    /// <param name="player">The player that is changing the knob.</param>
    public Scp914KnobChangingEventArgs(Scp914KnobSetting oldKnobSetting, Scp914KnobSetting knobSetting, Player player)
    {
        IsAllowed = true;
        OldKnobSetting = oldKnobSetting;
        KnobSetting = knobSetting;
        Player = player;
    }
    
    /// <summary>
    /// Gets the old knob setting used by SCP-914.
    /// </summary>
    public Scp914KnobSetting OldKnobSetting { get; }
    
    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; set; }

    /// <summary>
    /// The player that is changing the knob.
    /// </summary>
    public Player Player { get; }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
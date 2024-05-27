using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;
using Scp914;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when the knob of SCP-914 is changed.
/// </summary>
public class Scp914KnobChangedEventArgs : EventArgs, IScp914Event, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914KnobChangedEventArgs"/> class.
    /// </summary>
    /// <param name="oldKnobSetting">The old knob setting of SCP-914.</param>
    /// <param name="knobSetting">The new knob setting of SCP-914.</param>
    /// <param name="player">The player that has changed the knob.</param>
    public Scp914KnobChangedEventArgs(Scp914KnobSetting oldKnobSetting, Scp914KnobSetting knobSetting, Player player)
    {
        OldKnobSetting = oldKnobSetting;
        KnobSetting = knobSetting;
        Player = player;
    }
    
    /// <summary>
    /// Gets the old knob setting used by 914.
    /// </summary>
    public Scp914KnobSetting OldKnobSetting { get; }
    
    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; }

    /// <summary>
    /// The player that has changed the knob.
    /// </summary>
    public Player Player { get; }
}
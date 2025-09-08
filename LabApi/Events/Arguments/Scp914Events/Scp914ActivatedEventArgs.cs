using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Scp914;
using System;

namespace LabApi.Events.Arguments.Scp914Events;

/// <summary>
/// Represents the event arguments for when SCP-914 is activated.
/// </summary>
public class Scp914ActivatedEventArgs : EventArgs, IScp914Event, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp914ActivatedEventArgs"/> class.
    /// </summary>
    /// <param name="knobSetting">The knob setting of SCP-914.</param>
    /// <param name="hub">The player that activated SCP-914.</param>
    public Scp914ActivatedEventArgs(Scp914KnobSetting knobSetting, ReferenceHub hub)
    {
        KnobSetting = knobSetting;
        Player = Player.Get(hub);
    }

    /// <inheritdoc />
    public Scp914KnobSetting KnobSetting { get; }

    /// <summary>
    /// The player that activated SCP-914.
    /// </summary>
    public Player Player { get; }
}
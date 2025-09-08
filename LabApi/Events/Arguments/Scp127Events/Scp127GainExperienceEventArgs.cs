using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Events.Arguments.Scp127Events;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp127Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp127Events.GainExperience"/> event.
/// </summary>
public class Scp127GainExperienceEventArgs : EventArgs, IScp127ItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp127GainExperienceEventArgs"/> class.
    /// </summary>
    /// <param name="weapon">The Scp-127 firearm.</param>
    /// <param name="exp">The gained experience.</param>
    public Scp127GainExperienceEventArgs(Firearm weapon, float exp)
    {
        Scp127Item = (Scp127Firearm)FirearmItem.Get(weapon);
        ExperienceGain = exp;
    }

    /// <summary>
    /// Gets the SCP-127 firearm.
    /// </summary>
    public Scp127Firearm Scp127Item { get; }

    /// <summary>
    /// Gets the amount of experience the SCP-127 gained.
    /// </summary>
    public float ExperienceGain { get; }
}

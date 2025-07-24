using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp127Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp127Events.GainingExperience"/> event.
/// </summary>
public class Scp127GainingExperienceEventArgs : EventArgs, IScp127ItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp127GainingExperienceEventArgs"/> class.
    /// </summary>
    /// <param name="weapon">The Scp-127 firearm.</param>
    /// <param name="exp">The experience to be gained.</param>
    public Scp127GainingExperienceEventArgs(Firearm weapon, float exp)
    {
        Scp127Item = (Scp127Firearm)FirearmItem.Get(weapon);
        ExperienceGain = exp;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the SCP-127 firearm.
    /// </summary>
    public Scp127Firearm Scp127Item { get; }

    /// <summary>
    /// Gets or sets the amount of experience the SCP-127 will gain.
    /// </summary>
    public float ExperienceGain { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}

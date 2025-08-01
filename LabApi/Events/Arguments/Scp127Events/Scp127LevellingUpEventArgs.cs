using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules.Scp127;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp127Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp127Events.LevellingUp"/> event.
/// </summary>
public class Scp127LevellingUpEventArgs : EventArgs, IScp127ItemEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp127LevellingUpEventArgs"/> class.
    /// </summary>
    /// <param name="weapon">The Scp-127 firearm.</param>
    /// <param name="tier">The new tier.</param>
    public Scp127LevellingUpEventArgs(Firearm weapon, Scp127Tier tier)
    {
        Scp127Item = (Scp127Firearm)FirearmItem.Get(weapon);
        Tier = tier;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the SCP-127 firearm.
    /// </summary>
    public Scp127Firearm Scp127Item { get; }

    /// <summary>
    /// Gets the new tier of SCP-127.s
    /// </summary>
    public Scp127Tier Tier { get; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}

using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules.Scp127;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp127Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp127Events.LevelUp"/> event.
/// </summary>
public class Scp127LevelUpEventArgs : EventArgs, IScp127ItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp127LevelUpEventArgs"/> class.
    /// </summary>
    /// <param name="weapon">The Scp-127 firearm.</param>
    /// <param name="tier">The new tier.</param>
    public Scp127LevelUpEventArgs(Firearm weapon, Scp127Tier tier)
    {
        Scp127Item = (Scp127Firearm)FirearmItem.Get(weapon);
        Tier = tier;
    }

    /// <summary>
    /// Gets the SCP-127 firearm.
    /// </summary>
    public Scp127Firearm Scp127Item { get; }

    /// <summary>
    /// Gets the new tier of SCP-127.
    /// </summary>
    public Scp127Tier Tier { get; }
}

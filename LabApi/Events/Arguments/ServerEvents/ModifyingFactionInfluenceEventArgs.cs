using LabApi.Events.Arguments.Interfaces;
using PlayerRoles;
using Respawning;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ModifyingFactionInfluence"/> event.
/// </summary>
public class ModifyingFactionInfluenceEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModifyingFactionInfluenceEventArgs"/> class.
    /// </summary>
    /// <param name="faction">The faction whose influence is being modified.</param>
    /// <param name="influence">The <see cref="FactionInfluenceManager.Influence">influence</see> amount the <paramref name="faction"/> has now.</param>
    public ModifyingFactionInfluenceEventArgs(Faction faction, float influence)
    {
        Faction = faction;
        Influence = influence;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets or sets the faction whose influence is being modified.
    /// </summary>
    public Faction Faction { get; set; }

    /// <summary>
    /// Gets or sets the new influence of the <see cref="Faction"/>.
    /// </summary>
    public float Influence { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}

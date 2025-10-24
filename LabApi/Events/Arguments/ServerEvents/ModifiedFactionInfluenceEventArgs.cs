using PlayerRoles;
using Respawning;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.ModifiedFactionInfluence"/> event.
/// </summary>
public class ModifiedFactionInfluenceEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModifiedFactionInfluenceEventArgs"/> class.
    /// </summary>
    /// <param name="faction">The faction whose influence is modified.</param>
    /// <param name="influence">The <see cref="FactionInfluenceManager.Influence">influence</see> amount the <paramref name="faction"/> has now.</param>
    public ModifiedFactionInfluenceEventArgs(Faction faction, float influence)
    {
        Faction = faction;
        Influence = influence;
    }

    /// <summary>
    /// Gets the faction whose influence is modified.
    /// </summary>
    public Faction Faction { get; }

    /// <summary>
    /// Gets the new influence of the <see cref="Faction"/>.
    /// </summary>
    public float Influence { get; }
}

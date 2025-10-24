using PlayerRoles;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.AchievedMilestone"/> event.
/// </summary>
public class AchievedMilestoneEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AchievedMilestoneEventArgs"/> class.
    /// </summary>
    /// <param name="faction">The faction that achieved this milestone.</param>
    /// <param name="threshold">The influence threshold for this milestone.</param>
    /// <param name="milestoneIndex">The index of the achieved milestone.</param>
    public AchievedMilestoneEventArgs(Faction faction, int threshold, int milestoneIndex)
    {
        Faction = faction;
        Threshold = threshold;
        MilestoneIndex = milestoneIndex;
    }

    /// <summary>
    /// Gets the faction that achieved this milestone.
    /// </summary>
    public Faction Faction { get; }

    /// <summary>
    /// Gets the influence threshold for this milestone.
    /// </summary>
    public int Threshold { get; }

    /// <summary>
    /// Gets the index of the achieved milestone.
    /// </summary>
    public int MilestoneIndex { get; }
}
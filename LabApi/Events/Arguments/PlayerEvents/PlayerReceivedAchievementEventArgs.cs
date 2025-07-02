using Achievements;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using Mirror;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the argument for the <see cref="Handlers.PlayerEvents.ReceivedAchievement"/> event.
/// </summary>
public class PlayerReceivedAchievementEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivedAchievementEventArgs"/> class.
    /// </summary>
    /// <param name="identity">The <see cref="NetworkIdentity"/> component of the player.</param>
    /// <param name="name">The <see cref="AchievementName"/> being granted.</param>
    public PlayerReceivedAchievementEventArgs(NetworkIdentity identity, AchievementName name)
    {
        Player = Player.Get(identity);
        Achievement = name;
    }

    /// <summary>
    /// The player that getting the achievement.
    /// </summary>
    public Player? Player { get; }

    /// <summary>
    /// The <see cref="AchievementName"/> of the achievement.
    /// </summary>
    public AchievementName Achievement { get; }
}

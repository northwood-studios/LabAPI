using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using System;
using UnityEngine;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.GainedExperience"/> event.
/// </summary>
public class Scp079GainedExperienceEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079GainedExperienceEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="amount">The amount of experience gained.</param>
    /// <param name="reason">The reason of experience gain shown in HUD.</param>
    /// <param name = "subject" > The optional subject of the notification, used as replacement to display which class has been terminated</param>
    public Scp079GainedExperienceEventArgs(ReferenceHub player, float amount, Scp079HudTranslation reason, RoleTypeId subject)
    {
        Player = Player.Get(player);
        Amount = amount;
        Reason = reason;
        Subject = subject;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The amount of experience gained.
    /// </summary>
    public float Amount { get; }

    /// <summary>
    /// The reason of experience gain shown in HUD.
    /// </summary>
    public Scp079HudTranslation Reason { get; }

    /// <summary>
    /// Gets optional subject of the notification, used as replacement to display which class has been terminated.
    /// </summary>
    public RoleTypeId Subject { get; }
}

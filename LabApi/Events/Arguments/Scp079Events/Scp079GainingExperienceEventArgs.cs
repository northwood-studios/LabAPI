using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using System;

namespace LabApi.Events.Arguments.Scp079Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp079Events.GainingExperience"/> event.
/// </summary>
public class Scp079GainingExperienceEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp079GainingExperienceEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-079 player instance.</param>
    /// <param name="amount">The amount of experience that is going to be gained.</param>
    /// <param name="reason">The reason of experience gain that is going to be shown in HUD.</param>
    /// <param name="subject">The optional subject of the notification, used as replacement to display which class has been terminated.</param>
    public Scp079GainingExperienceEventArgs(ReferenceHub player, float amount, Scp079HudTranslation reason, RoleTypeId subject)
    {
        Player = Player.Get(player);
        Amount = amount;
        Reason = reason;
        Subject = subject;
        IsAllowed = true;
    }

    /// <summary>
    /// Gets the SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the amount of experience that is going to be gained.
    /// </summary>
    public float Amount { get; set; }

    /// <summary>
    /// Gets or sets the reason of experience gain that is going to be shown in HUD.
    /// </summary>
    public Scp079HudTranslation Reason { get; set; }

    /// <summary>
    /// Gets or sets the optional subject of the notification, used as replacement to display which class has been terminated.
    /// </summary>
    public RoleTypeId Subject { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}

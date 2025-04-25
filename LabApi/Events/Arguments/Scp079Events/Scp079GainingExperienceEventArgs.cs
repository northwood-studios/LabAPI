using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
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
    public Scp079GainingExperienceEventArgs(ReferenceHub player, float amount, Scp079HudTranslation reason)
    {
        Player = Player.Get(player);
        Amount = amount;
        Reason = reason;
        IsAllowed = true;
    }

    /// <summary>
    /// The SCP-079 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The amount of experience that is going to be gained.
    /// </summary>
    public float Amount { get; }

    /// <summary>
    /// The reason of experience gain that is going to be shown in HUD.
    /// </summary>
    public Scp079HudTranslation Reason { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}

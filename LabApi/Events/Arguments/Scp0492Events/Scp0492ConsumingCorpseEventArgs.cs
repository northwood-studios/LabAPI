using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;

namespace LabApi.Events.Arguments.Scp0492Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp0492Events.ConsumingCorpse"/> event.
/// </summary>
public class Scp0492ConsumingCorpseEventArgs : EventArgs, IPlayerEvent, IRagdollEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp0492ConsumingCorpseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is consuming the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll that is being consumed.</param>
    /// <param name="healAmount">The amount of health to heal.</param>
    public Scp0492ConsumingCorpseEventArgs(ReferenceHub hub, BasicRagdoll ragdoll, float healAmount)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Ragdoll = Ragdoll.Get(ragdoll);
        HealAmount = healAmount;
    }

    /// <summary>
    /// Gets or sets the amount of health to heal.
    /// </summary>
    public float HealAmount { get; set; }

    /// <summary>
    /// Gets or sets whether to add the consumed ragdoll to the list of consumed ragdolls.
    /// </summary>
    public bool AddToConsumedRagdollList { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to heal the player if the ragdoll has already been consumed.
    /// </summary>
    public bool HealIfAlreadyConsumed { get; set; } = false;

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public Ragdoll Ragdoll { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
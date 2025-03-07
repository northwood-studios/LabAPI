using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp049.Zombies;
using PlayerRoles.Ragdolls;

namespace LabApi.Events.Arguments.Scp0492Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp0492Events.StartingConsumingCorpse"/> event.
/// </summary>
public class Scp0492StartingConsumingCorpseEventArgs : EventArgs, IPlayerEvent, IRagdollEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp0492StartingConsumingCorpseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is starting to consume the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll that is starting to be consumed.</param>
    /// <param name="error">The <see cref="ZombieConsumeAbility.ConsumeError"/>.</param>
    public Scp0492StartingConsumingCorpseEventArgs(ReferenceHub hub, BasicRagdoll ragdoll, ZombieConsumeAbility.ConsumeError error)
    {
        Player = Player.Get(hub);
        Ragdoll = Ragdoll.Get(ragdoll);
        Error = error;
    }

    /// <summary>
    /// Gets or sets the <see cref="ZombieConsumeAbility.ConsumeError"/>.
    /// </summary>
    /// <remarks>Anything other than <see cref="ZombieConsumeAbility.ConsumeError.None"/> will cancel the event.</remarks>
    public ZombieConsumeAbility.ConsumeError Error { get; set; }

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public Ragdoll Ragdoll { get; }

    /// <inheritdoc />
    public bool IsAllowed
    {
        get => Error == ZombieConsumeAbility.ConsumeError.None;
        set => Error = value ? ZombieConsumeAbility.ConsumeError.None : ZombieConsumeAbility.ConsumeError.TargetNotValid;
    }
}
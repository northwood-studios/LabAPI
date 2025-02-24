using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;

namespace LabApi.Events.Arguments.Scp0492Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp0492Events.ConsumedCorpse"/> event.
/// </summary>
public class Scp0492ConsumedCorpseEventArgs : EventArgs, IPlayerEvent, IRagdollEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp0492ConsumedCorpseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who consumed the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll that was consumed.</param>
    public Scp0492ConsumedCorpseEventArgs(ReferenceHub hub, BasicRagdoll ragdoll)
    {
        Player = Player.Get(hub);
        Ragdoll = Ragdoll.Get(ragdoll);
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public Ragdoll Ragdoll { get; }
}
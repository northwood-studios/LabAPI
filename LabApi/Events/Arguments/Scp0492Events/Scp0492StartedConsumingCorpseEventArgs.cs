using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;
using System;

namespace LabApi.Events.Arguments.Scp0492Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp0492Events.StartedConsumingCorpse"/> event.
/// </summary>
public class Scp0492StartedConsumingCorpseEventArgs : EventArgs, IPlayerEvent, IRagdollEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp0492StartedConsumingCorpseEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who started consuming the ragdoll.</param>
    /// <param name="ragdoll">The ragdoll that started to be consumed.</param>
    public Scp0492StartedConsumingCorpseEventArgs(ReferenceHub hub, BasicRagdoll ragdoll)
    {
        Player = Player.Get(hub);
        Ragdoll = Ragdoll.Get(ragdoll);
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <inheritdoc />
    public Ragdoll Ragdoll { get; }
}
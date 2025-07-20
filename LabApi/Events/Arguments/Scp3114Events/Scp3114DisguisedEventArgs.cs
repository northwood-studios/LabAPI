using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.Ragdolls;
using System;

namespace LabApi.Events.Arguments.Scp3114Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp3114Events.Disguised"/> event.
/// </summary>
public class Scp3114DisguisedEventArgs : EventArgs, IPlayerEvent, IRagdollEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp3114DisguisedEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player SCP who disguised./param>
    /// <param name="ragdoll">The ragdoll used for disguising purposes.</param>
    public Scp3114DisguisedEventArgs(ReferenceHub player, BasicRagdoll ragdoll)
    {
        Player = Player.Get(player);
        Ragdoll = Ragdoll.Get(ragdoll);
    }

    /// <inheritdoc/>
    public Player Player { get; }

    /// <inheritdoc/>
    public Ragdoll Ragdoll { get; }
}

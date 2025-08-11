using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp106Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp106Events.ChangingSubmersionStatus"/> event.
/// </summary>
public class Scp106ChangedSubmersionStatusEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp106ChangedSubmersionStatusEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-106 player instance.</param>
    /// <param name="isSubmerging">Whether the SCP-106 is submerging or emerging.</param>
    public Scp106ChangedSubmersionStatusEventArgs(ReferenceHub player, bool isSubmerging)
    {
        Player = Player.Get(player);
        IsSubmerging = isSubmerging;
    }

    /// <summary>
    /// Whether the SCP-106 is submerging or emerging.
    /// </summary>
    public bool IsSubmerging { get; }

    /// <summary>
    /// The SCP-106 player instance.
    /// </summary>
    public Player Player { get; }
}
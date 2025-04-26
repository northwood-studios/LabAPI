using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp173;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.PlayingSound"/> event.
/// </summary>
public class Scp173PlayingSoundEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// The sound id that is going to be played.
    /// </summary>
    /// <param name="player">The SCP-173 player instance.</param>
    /// <param name="soundId">The sound id that is going to be played.</param>
    public Scp173PlayingSoundEventArgs(ReferenceHub player, Scp173AudioPlayer.Scp173SoundId soundId)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        SoundId = soundId;
    }

    /// <summary>
    /// The sound id that is going to be played.
    /// </summary>
    public Scp173AudioPlayer.Scp173SoundId SoundId { get; set; }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp173;
using System;

namespace LabApi.Events.Arguments.Scp173Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp173Events.PlayedSound"/> event.
/// </summary>
public class Scp173PlayedSoundEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// The sound id that is being played.
    /// </summary>
    /// <param name="player">The SCP-173 player instance.</param>
    /// <param name="soundId">The sound id that is being played.</param>
    public Scp173PlayedSoundEventArgs(ReferenceHub player, Scp173AudioPlayer.Scp173SoundId soundId)
    {
        Player = Player.Get(player);
        SoundId = soundId;
    }

    /// <summary>
    /// The sound id that is being played.
    /// </summary>
    public Scp173AudioPlayer.Scp173SoundId SoundId { get; }

    /// <summary>
    /// The SCP-173 player instance.
    /// </summary>
    public Player Player { get; }

}
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SentHitmarker"/> event.
/// </summary>
public class PlayerSentHitmarkerEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance for the <see cref="PlayerSentHitmarkerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that sent the hitmarker.</param>
    /// <param name="size">The target size multiplier.</param>
    /// <param name="playedAudio">Whether the hitmarker sound effect was played.</param>
    /// <param name="hitmarkerType">The type of the hitmarker.</param>
    public PlayerSentHitmarkerEventArgs(ReferenceHub hub, float size, bool playedAudio, HitmarkerType hitmarkerType)
    {
        Player = Player.Get(hub);
        Size = size;
        PlayedAudio = playedAudio;
        Hitmarker = hitmarkerType;
    }

    /// <summary>
    /// Gets the player that the hitmarker was sent to.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the target size multiplier.
    /// </summary>
    public float Size { get; }

    /// <summary>
    /// Gets a value indicating whether the hitmarker sound effect was played.
    /// </summary>
    public bool PlayedAudio { get; }

    /// <summary>
    /// Gets the type of the hitmarker.
    /// </summary>
    public HitmarkerType Hitmarker { get; }
}

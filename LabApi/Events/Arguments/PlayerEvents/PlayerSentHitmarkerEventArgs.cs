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
    /// <param name="hub">The player that sent the hitmark.</param>
    /// <param name="size">The target size multiplier.</param>
    /// <param name="playAudio">Whether the hitmarker sound effect play.</param>
    public PlayerSentHitmarkerEventArgs(ReferenceHub hub, float size, bool playAudio)
    {
        Player = Player.Get(hub);
        Size = size;
        PlayAudio = playAudio;
    }

    /// <summary>
    /// Gets the player that the hitmarker is being sent to.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the target size multiplier.
    /// </summary>
    public float Size { get; }

    /// <summary>
    /// Gets if the hitmarker sound effect play.
    /// </summary>
    public bool PlayAudio { get; }
}
